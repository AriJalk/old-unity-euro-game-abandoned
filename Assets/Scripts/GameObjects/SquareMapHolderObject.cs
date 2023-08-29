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

    public void Initialize(MapGrid map)
    {
        this.map = map;
        grid = new GameObject("Grid");
        grid.transform.SetParent(transform); // Set the parent transform
        grid.transform.localScale = Vector3.one;
    }


    public MapTile GetDataTile(int row, int col)
    {
        return (MapTile)map.GetCell(col, row);
    }

    public void SetTile(SquareTileObject tile)
    {
        map.SetCell(tile.TileData);
    }

    public MapGrid GetMap()
    {
        return map;
    }


    public GameObject GetGridObject()
    {
        return grid;
    }
}