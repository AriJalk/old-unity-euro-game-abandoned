using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using ResourcePool;
using Unity.VisualScripting;

public class SquareMapHolderObject : MonoBehaviour
{
    public MapRenderer MapRenderer;
    private GameObject grid;
    private SquareMap map;
   

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
        map = new SquareMap();
        grid = new GameObject("Grid");
        grid.transform.SetParent(transform); // Set the parent transform
        grid.transform.localScale = Vector3.one;
    }


    public SquareTile GetDataTile(int row, int col)
    {
        return map.GetTile(row, col);
    }

    public void SetTile(int row, int col, SquareTileObject tile)
    {
        map.SetTile(row, col, tile.TileData);
    }

    public SquareMap GetMap()
    {
        return map;
    }

    public GameObject GetGrid()
    {
        return grid;
    }
}