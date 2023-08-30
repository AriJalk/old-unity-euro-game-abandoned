using EDBG.MapSystem;
using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsRenderer : MonoBehaviour
{
    private PoolManager poolManager;
    public GameObject DiscPrefab;
    public float DiscScale = 1;

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
        DiscPrefab = Resources.Load<GameObject>("Prefabs/3D/DiscPrefab");
        if (DiscPrefab == null)
        {
            Debug.Log("DISC PREFAB NOT FOUND");
            return;
        }
        poolManager.RegisterPrefab<DiscObject>(DiscPrefab);
    }

    public void RenderObjectsOnMap(SquareTileObject[,] tiles, MaterialPool materialPool)
    {
        foreach (SquareTileObject tile in tiles)
        {
            RenderObjectsOnTileObject(tile, materialPool);
        }
    }

    public void RenderObjectsOnTileObject(SquareTileObject tile, MaterialPool materialPool)
    {
        RemovePreviousDiscs(tile);
        CreateNewDiscs(tile, materialPool);
    }

    private void CreateNewDiscs(SquareTileObject tile, MaterialPool materialPool)
    {

        float tileHeight = SquareTileObject.TILE_HEIGHT;
        float discHeight = DiscObject.DISC_HEIGHT;
        float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc
        //float gridCellSize = SquareTileObject.TILE_LENGTH / 3;
        if (tile.TileData.ComponentOnTile is GameStack<Disc> discStack && discStack.Count > 0)
        {
            for (int i = 0; i < discStack.Count; i++)
            {
                DiscObject newDisc = poolManager.RetrievePoolObject<DiscObject>();
                newDisc.enabled = true;
                newDisc.discData = discStack.GetItemByIndex(i);
                newDisc.transform.SetParent(tile.transform);
                newDisc.transform.localScale = Vector3.one * DiscScale;
                newDisc.transform.localPosition = new Vector3(0, ((i * discHeight) + initialHeightOffset) * DiscScale + tileHeight, 0);

                // Apply Material based on disc color
                switch (newDisc.discData.DiscColor)
                {
                    case EDBG.Rules.Colors.Blue:
                        newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/BlueWoodMaterial"));
                        break;
                    case EDBG.Rules.Colors.Red:
                        newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/RedWoodMaterial"));
                        break;
                    case EDBG.Rules.Colors.Green:
                        newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/GreenWoodMaterial"));
                        break;
                    case EDBG.Rules.Colors.White:
                        newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/OrangeWoodMaterial"));
                        break;

                }
            }
        }
    }

    private void RemovePreviousDiscs(SquareTileObject tile)
    {
        DiscObject[] discs = tile.GetComponentsInChildren<DiscObject>();
        foreach (DiscObject disc in discs)
        {
            poolManager.ReturnPoolObject(disc);
        }
    }
}
