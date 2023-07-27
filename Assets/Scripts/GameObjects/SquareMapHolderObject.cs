using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using ResourcePool;
using Unity.VisualScripting;

public class SquareMapHolderObject : MonoBehaviour
{
    private GameObject grid;
    private MapGrid map;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        map = new MapGrid();
        grid = new GameObject("Grid");
        grid.transform.SetParent(transform); // Set the parent transform
        grid.transform.localScale = Vector3.one;
    }


    public MapTile GetDataTile(int row, int col)
    {
        return (MapTile)map.GetCell(row, col);
    }

    public void SetTile(int row, int col, SquareTileObject tile)
    {
        map.SetCell(row, col, tile.TileData);
    }

    public MapGrid GetMap()
    {
        return map;
    }

    public GameObject GetGrid()
    {
        return grid;
    }
}