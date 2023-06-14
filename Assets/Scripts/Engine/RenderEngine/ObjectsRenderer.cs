using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsRenderer : MonoBehaviour
{
    private PoolManager poolManager;
    public GameObject DiscPrefab;

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
        GameStack<Disc> discStack = tile.TileData.discStack; // Assuming only one disc stack per tile

        float tileHeight = SquareTileObject.TILE_HEIGHT;
        float discHeight = DiscObject.DISC_HEIGHT;
        float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc

        for (int i = 0; i < 2; i++)
        {
            float xOffset;
            if (i == 0)
            {
                xOffset = -SquareTileObject.TILE_LENGTH / 4;
            }
            else
            {
                xOffset = SquareTileObject.TILE_LENGTH / 4;
            }
            for (int j = 0; j < 2; j++)
            {
                float zOffset;
                if (j == 0)
                {
                    zOffset = -SquareTileObject.TILE_LENGTH / 4;
                }
                else
                {
                    zOffset = SquareTileObject.TILE_LENGTH / 4;
                }

                for (int k = 0; k < tile.TileData.DiscStacks[i, j].Count; k++)
                {
                    DiscObject newDisc = poolManager.RetrievePoolObject<DiscObject>();
                    newDisc.enabled = true;
                    newDisc.discData = tile.TileData.DiscStacks[i, j].GetItemByIndex(k);
                    newDisc.transform.SetParent(tile.transform);
                    newDisc.transform.localScale = Vector3.one;
                    newDisc.transform.localPosition = new Vector3(xOffset, (k * discHeight) + initialHeightOffset + tileHeight, zOffset);

                    // Apply Material based on disc color
                    switch (newDisc.discData.DiscColor)
                    {
                        case Disc.DiscColorPalette.Blue:
                            newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/BlueWoodMaterial"));
                            break;
                        case Disc.DiscColorPalette.Red:
                            newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/RedWoodMaterial"));
                            break;
                        case Disc.DiscColorPalette.Green:
                            newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/GreenWoodMaterial"));
                            break;
                        case Disc.DiscColorPalette.White:
                            newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/YellowMaterial"));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        for (int i = 0; i < discStack.Count; i++)
        {
            DiscObject newDisc = poolManager.RetrievePoolObject<DiscObject>();
            newDisc.enabled = true;
            newDisc.discData = tile.TileData.discStack.GetItemByIndex(i);
            newDisc.transform.SetParent(tile.transform);
            newDisc.transform.localScale = Vector3.one;
            newDisc.transform.localPosition = new Vector3(0f, (i * discHeight) + initialHeightOffset + tileHeight, 0f);

            // Apply Material based on disc color
            switch (newDisc.discData.DiscColor)
            {
                /*case Disc.DiscColorPalette.Blue:
                    newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/BlueWoodMaterial"));
                    break;
                case Disc.DiscColorPalette.Red:
                    newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/RedWoodMaterial"));
                    break;
                case Disc.DiscColorPalette.Green:
                    newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/GreenWoodMaterial"));
                    break;
                case Disc.DiscColorPalette.White:
                    newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/YellowMaterial"));
                    break;*/
                default:
                    newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/WoodMaterial"));
                    break;
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
