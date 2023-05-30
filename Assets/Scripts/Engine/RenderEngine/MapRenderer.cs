
using UnityEngine;
using ResourcePool;

public class MapRenderer : MonoBehaviour
{
    private PoolManager poolManager;
    public GameObject SquarePrefab;
    private SquareTileObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(PoolManager pm)
    {
        poolManager = pm;
        SquarePrefab = Resources.Load<GameObject>("Prefabs/SquareTilePrefab");
        if (SquarePrefab == null)
        {
            Debug.Log("Square Tile Prefab is not found in Resources/Prefabs/SquareTilePrefab.");
            return;
        }
        poolManager.RegisterPrefab<SquareTileObject>(SquarePrefab);
    }

    public void RenderMap(SquareMapHolderObject mapHolder, MaterialPool materialPool, DiscRenderer discRenderer)
    {
        tiles = new SquareTileObject[mapHolder.GetMap().Rows, mapHolder.GetMap().Columns];
        RemovePreviousTiles(mapHolder.GetGrid().transform);
        for (int row = 0; row < mapHolder.GetMap().Rows; row++)
        {
            for (int col = 0; col < mapHolder.GetMap().Columns; col++)
            {
                SquareTileObject tile = poolManager.RetrievePoolObject<SquareTileObject>();
                tile.TileData = mapHolder.GetMap().GetTile(row, col);
                tile.transform.SetParent(mapHolder.GetGrid().transform);
                tile.transform.localScale = Vector3.one;
                Vector3 position = new Vector3(row * (SquareTileObject.TILE_SPACING + SquareTileObject.TILE_LENGTH)
                    , 0.1f
                    , col * (SquareTileObject.TILE_SPACING + SquareTileObject.TILE_LENGTH));
                tile.transform.localPosition = position;
                tile.gameObject.isStatic = true;
                tile.gameObject.SetActive(true);
                tile.name = "Tile [" + row + "," + col + "]";
                tiles[row, col] = tile;
                discRenderer.RenderDiscsOnTileObject(tile, materialPool);
            }
        }
    }

    public SquareTileObject GetTileObject(int row, int col)
    {
        return tiles[row, col];
    }

    public void SetTileObjectAndRender(SquareTile tile)
    {
        if (tile == null)
        {
            SquareTileObject squareTileObject = poolManager.RetrievePoolObject<SquareTileObject>();
            squareTileObject.TileData = tile;

        }
        else
            Debug.LogError("Cant render tile, tile is NULL");
    }

    private void RemovePreviousTiles(Transform grid)
    {
        SquareTileObject[] tiles = grid.GetComponentsInChildren<SquareTileObject>();
        foreach (SquareTileObject tile in tiles)
        {
            poolManager.ReturnPoolObject(tile);
        }
    }
}