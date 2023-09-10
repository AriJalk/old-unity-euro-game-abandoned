using EDBG.MapSystem;
using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsRenderer : MonoBehaviour
{
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

    public void RenderObjectsOnMap(SquareTileObject[,] tiles)
    {
        foreach (SquareTileObject tile in tiles)
        {
            RenderObjectsOnTileObject(tile);
        }
    }

    public void RenderObjectsOnTileObject(SquareTileObject tile)
    {
        RemovePreviousDiscs(tile);
        CreateNewDiscs(tile);
    }


    private void RemovePreviousDiscs(SquareTileObject tile)
    {
        DiscObject[] discs = tile.GetComponentsInChildren<DiscObject>();
        foreach (DiscObject disc in discs)
        {
            GameEngineManager.Instance.PoolManager.ReturnPoolObject(disc);
        }
    }

    private void CreateNewDiscs(SquareTileObject tile)
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
                        newDisc.ApplyMaterial(GameEngineManager.Instance.MaterialManager.GetMaterial("Materials/BlueWoodMaterial"));
                        break;
                    case EDBG.Rules.Colors.Red:
                        newDisc.ApplyMaterial(GameEngineManager.Instance.MaterialManager.GetMaterial("Materials/RedWoodMaterial"));
                        break;
                    case EDBG.Rules.Colors.Green:
                        newDisc.ApplyMaterial(GameEngineManager.Instance.MaterialManager.GetMaterial("Materials/GreenWoodMaterial"));
                        break;
                    case EDBG.Rules.Colors.White:
                        newDisc.ApplyMaterial(GameEngineManager.Instance.MaterialManager.GetMaterial("Materials/OrangeWoodMaterial"));
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
                    newDisc.ApplyMaterial(GameEngineManager.Instance.MaterialManager.GetMaterial("Materials/WhiteMaterial"));
                }
            }
        }
    }
}
