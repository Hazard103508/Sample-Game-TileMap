using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    #region Objects
    [SerializeField] private Prefabs prefabs;
    [SerializeField] private GameObject Environment;

    private List<TileInfo> lstTileInfo;
    #endregion

    #region Properties
    /// <summary>
    /// Personaje jugable
    /// </summary>
    public Player Player { get; private set; }
    /// <summary>
    /// Tamaño del mapa
    /// </summary>
    public Rect Rectangle { get; private set; }

    /// <summary>
    /// Lista de Tiles existentes en el mapa
    /// </summary>
    public List<Tile> Tiles { get; private set; }
    #endregion


    #region Unity Methods
    private void Start()
    {
        this.Tiles = new List<Tile>();

        GameObject folder_Walls = new GameObject("Walls");
        GameObject folder_Floors = new GameObject("Floors");
        GameObject folder_Bombs = new GameObject("Bombs");
        GameObject folder_Diamonds = new GameObject("Diamonds");
        GameObject folder_Doors = new GameObject("Doors");

        folder_Walls.transform.parent =
        folder_Floors.transform.parent =
        folder_Bombs.transform.parent =
        folder_Diamonds.transform.parent =
        folder_Doors.transform.parent = Environment.transform;

        // creo una lista con la informacion de todos los tiles existentes y su correspondiente id en el json del mapa
        lstTileInfo = new List<TileInfo>();
        lstTileInfo.Add(new TileInfo(1, TileType.Wall, folder_Walls.transform, prefabs.Wall));
        lstTileInfo.Add(new TileInfo(2, TileType.Floor, folder_Floors.transform, prefabs.Floor));
        lstTileInfo.Add(new TileInfo(3, TileType.Gem, folder_Diamonds.transform, prefabs.Diamond_blue));
        lstTileInfo.Add(new TileInfo(4, TileType.Player, Environment.transform, prefabs.Player));
    }
    #endregion

    #region Methods
    /// <summary>
    /// Carga el mapa 
    /// </summary>
    /// <param name="config">Configuracion web de la actividad</param>
    /// <param name="callback">Funcion a ejecutar finalizada la carga del mapa</param>
    public void Load()
    {
        var asset = Resources.Load<TextAsset>("Level1");
        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<MapData>(asset.text);

        Load_Layer(data.layers[0]);
        Load_Layer(data.layers[1]);

        this.Rectangle = new Rect(0, 0, data.layers[0].width, data.layers[0].height);
    }
    /// <summary>
    /// Carga un layer del mapa
    /// </summary>
    /// <param name="layer">layer a cargar</param>
    private void Load_Layer(MapData.Layer layer)
    {
        for (int i = 0; i < layer.data.Count; i++)
        {
            int id = layer.data[i];
            if (id != 0)
            {
                int x = i % layer.width;
                int y = i / layer.width;

                var tileInfo = lstTileInfo[id - 1];

                var go = Instantiate(tileInfo.Prefab, tileInfo.Folder);
                go.transform.localPosition = new Vector3(x, -y, 0);

                var _tile = go.GetComponent<Tile>();
                _tile.Location = new Vector2Int(x, -y);
                this.Tiles.Add(_tile);

                if (tileInfo.Type == TileType.Player)
                {
                    this.Player = (Player)_tile;
                    this.Player.gameObject.name = "Player";
                }
            }
        }
    }
    #endregion

    #region Strcutures
    class TileInfo
    {
        public TileInfo(int id, TileType type, Transform folder, GameObject prefab)
        {
            this.Id = Id;
            this.Type = type;
            this.Folder = folder;
            this.Prefab = prefab;
        }

        /// <summary>
        /// Id del Tile
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Tipo de Tile
        /// </summary>
        public TileType Type { get; private set; }
        /// <summary>
        /// Objeto padre en la herarquia
        /// </summary>
        public Transform Folder { get; private set; }
        /// <summary>
        /// Prefab a instanciar para este tile
        /// </summary>
        public GameObject Prefab { get; set; }
    }
    [Serializable]
    public class Prefabs
    {
        public GameObject Wall;
        public GameObject Floor;
        public GameObject Player;
        public GameObject Diamond_blue;
    }
    #endregion
}
