using EDBG.MapSystem;
using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsRenderer : MonoBehaviour
{
    private static ObjectsRenderer instance;
    public static ObjectsRenderer Instance
    {
        get
        {
            if (instance == null)
            {
                // Try to find an existing instance in the scene
                instance = FindObjectOfType<ObjectsRenderer>();

                if (instance == null)
                {
                    // If no instance exists, create a new GameObject with PoolManager and attach it
                    GameObject managerObject = new GameObject("ObjectRenderer");
                    instance = managerObject.AddComponent<ObjectsRenderer>();
                }

                // Ensure the instance persists across scenes
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }


    //TODO: range
    public float DiscScale = 1;

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
        
    }

    public void RenderObjectsOnMap(SquareTileObject[,] tiles, MaterialManager materialPool)
    {
        foreach (SquareTileObject tile in tiles)
        {
            RenderObjectsOnTileObject(tile, materialPool);
        }
    }

    public void RenderObjectsOnTileObject(SquareTileObject tile, MaterialManager materialPool)
    {
        RemovePreviousDiscs(tile);
        CreateNewDiscs(tile, materialPool);
    }


    private void RemovePreviousDiscs(SquareTileObject tile)
    {
        DiscObject[] discs = tile.GetComponentsInChildren<DiscObject>();
        foreach (DiscObject disc in discs)
        {
            GameEngineManager.Instance.PoolManager.ReturnPoolObject(disc);
        }
    }

    private void CreateNewDiscs(SquareTileObject tile, MaterialManager materialPool)
    {

        float tileHeight = SquareTileObject.TILE_HEIGHT;
        float discHeight = DiscObject.DISC_HEIGHT * DiscScale;
        float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc
        float fillerDiscFactor = 6;
        float fillerDiscHeight = discHeight / fillerDiscFactor;
        //float gridCellSize = SquareTileObject.TILE_LENGTH / 3;
        if (tile.TileData.ComponentOnTile is GameStack<Disc> discStack && discStack.Count > 0)
        {
            for (int i = 0; i < discStack.Count; i++)
            {
                DiscObject newDisc = GameEngineManager.Instance.PoolManager.RetrievePoolObject<DiscObject>();
                newDisc.enabled = true;
                newDisc.discData = discStack.GetItemByIndex(i);
                newDisc.transform.SetParent(tile.transform);
                newDisc.transform.localScale = Vector3.one * DiscScale;
                Vector3 position = new Vector3(0, i * discHeight + initialHeightOffset + tileHeight + ((i != 0) ? fillerDiscHeight * i : 0), 0);
                newDisc.transform.localPosition = position;

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
                //Create filler disc
                if (i < discStack.Count - 1)
                {
                    newDisc = GameEngineManager.Instance.PoolManager.RetrievePoolObject<DiscObject>();
                    newDisc.transform.SetParent(tile.transform);
                    newDisc.transform.localScale = new Vector3(DiscScale, DiscScale / fillerDiscFactor, DiscScale);
                    float fillerYPos = position.y + discHeight;
                    newDisc.transform.localPosition = new Vector3(0, fillerYPos, 0);
                    newDisc.ApplyMaterial(materialPool.GetMaterial("Materials/WhiteMaterial"));
                }
            }
        }
    }
}
