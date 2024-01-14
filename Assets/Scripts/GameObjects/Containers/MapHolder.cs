using EDBG.Utilities.DataTypes;
using UnityEngine;


public class MapHolder : MonoBehaviour
{
    private MapTileGameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(int rows, int columns)
    {
        tiles = new MapTileGameObject[rows,columns];
        //Load tiles
        foreach(Transform child in transform)
        {
            MapTileGameObject tile = child.GetComponent<MapTileGameObject>();
            if (tile != null)
            {
                tiles[tile.TileData.GamePosition.Row, tile.TileData.GamePosition.Col] = tile;
            }
        }
    }

    public void SetTiles(MapTileGameObject[,] tiles)
    {
        this.tiles = tiles;
    }

    public MapTileGameObject GetTile(GamePosition position)
    {
        return tiles[position.Row, position.Col];
    }
}