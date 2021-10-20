using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Objects
    [SerializeField] private PlayerFollower playerFollower;
    [SerializeField] private Map map;
    [SerializeField] private Text CounterText;

    private int counter;
    #endregion

    #region Unity Events 
    void Start()
    {
        this.map.Load();
        playerFollower.Set_Map(this.map);

        var lstGems = this.map.Tiles.Where(x => x.type == TileType.Gem).Select(x => (Gem)x).ToList();
        lstGems.ForEach(x => x.Collected.AddListener(() => counter++));
    }

    private void Update()
    {
        CounterText.text = counter.ToString();
        map.Player.Direction = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
    }
    #endregion
}
