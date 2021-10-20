using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    #region Objects
    [SerializeField] private RectTransform bounds; // para cambiar los limites que aplica la camara se debe cambiar el tamaño de este objeto en el canvas
    private Map map;
    private Camera cam;
    private bool isZooming;

    MapBounds mapBounds;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        cam = GetComponent<Camera>();
        mapBounds = new MapBounds();
    }
    private void LateUpdate()
    {
        if (map != null && map.Player != null)
        {
            this.transform.position = new Vector3(
                 Mathf.Clamp(map.Player.transform.position.x, mapBounds.Left, mapBounds.Right),
                 Mathf.Clamp(map.Player.transform.position.y, mapBounds.Bottom, mapBounds.Top),
                 this.transform.position.z
             );
        }
        else
            this.transform.position = new Vector3(0, 0, this.transform.position.z);

    }
    #endregion

    #region Other Methods
    /// <summary>
    /// Setea el mapa cargado en la escena
    /// </summary>
    /// <param name="map">Mapa jugable</param>
    public void Set_Map(Map map)
    {
        this.map = map;
        Set_Bounds();
    }
    /// <summary>
    /// Setea los limites de desplazamiento de la camara
    /// </summary>
    private void Set_Bounds()
    {
        // Utilizo un objeto del UI para limitar el espacio de desplazamiento de la camara en relacion al mapa
        // Posicion en el espacio de cada limite inicial
        Vector2 resolutionDesing = new Vector2(1920, 1080);
        Vector2 resolutionFactor = new Vector2(Screen.width, Screen.height) / resolutionDesing;
        var topLeft = cam.ScreenToWorldPoint(new Vector2(bounds.GetLeft(), resolutionDesing.y - bounds.GetTop()) * resolutionFactor);
        var bottomRight = cam.ScreenToWorldPoint(new Vector2(resolutionDesing.x - bounds.GetRight(), bounds.GetBottom()) * resolutionFactor);

        // se ajustan los limites al tamaño del mapa
        mapBounds.Top = 0.5f - topLeft.y + this.transform.position.y;
        mapBounds.Bottom = -map.Rectangle.height + 0.5f - bottomRight.y + this.transform.position.y;
        mapBounds.Left = -0.5f - topLeft.x + this.transform.position.x;
        mapBounds.Right = map.Rectangle.width - 0.5f - bottomRight.x + this.transform.position.x;

        // si el mapa es demaciado angosto se fija la cama al centro
        if (map.Rectangle.width < (bottomRight.x - topLeft.x))
            mapBounds.Right = mapBounds.Left = (mapBounds.Right + mapBounds.Left) / 2;

        if (map.Rectangle.height < (topLeft.y - bottomRight.y))
            mapBounds.Top = mapBounds.Bottom = (mapBounds.Top + mapBounds.Bottom) / 2;
    }
    #endregion

    #region Structures
    class MapBounds
    {
        /// <summary>
        /// Posición izquierda del mapa (word position)
        /// </summary>
        public float Left { get; set; }
        /// <summary>
        /// Posición derecha del mapa (word position)
        /// </summary>
        public float Right { get; set; }
        /// <summary>
        /// Posición superior del mapa (word position)
        /// </summary>
        public float Top { get; set; }
        /// <summary>
        /// Posición inferior del mapa (word position)
        /// </summary>
        public float Bottom { get; set; }
    }
    #endregion
}