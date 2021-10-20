using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType type;

    public Vector2Int Location { get; set; }
}
public enum TileType
{
    Wall,
    Floor,
    Gem,
    Player
}
