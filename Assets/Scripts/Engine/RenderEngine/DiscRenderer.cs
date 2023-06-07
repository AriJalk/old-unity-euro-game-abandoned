using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscRenderer : MonoBehaviour
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

    public void RenderDiscsOnMap(SquareTileObject[,] tiles, MaterialPool materialPool)
    {
        foreach (SquareTileObject tile in tiles)
        {
            RenderDiscsOnTileObject(tile, materialPool);
        }
    }

    public void RenderDiscsOnTileObject(SquareTileObject tile, MaterialPool materialPool)
    {
        RemovePreviousDiscs(tile);
        CreateNewDiscs(tile, materialPool);
    }

    private void CreateNewDiscs(SquareTileObject tile, MaterialPool materialPool)
    {
        DiscStack discStack = tile.TileData.discStack; // Assuming only one disc stack per tile

        float tileHeight = SquareTileObject.TILE_HEIGHT;
        float discHeight = DiscObject.DISC_HEIGHT;
        float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc

        for (int i = 0; i < discStack.Count; i++)
        {
            DiscObject newDisc = poolManager.RetrievePoolObject<DiscObject>();
            newDisc.discData = discStack.GetDiscByIndex(i);
            newDisc.transform.SetParent(tile.transform);
            newDisc.transform.localScale = Vector3.one;

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

            // Calculate the position for the new disc
            Vector3 position = new Vector3(0f, i * discHeight + initialHeightOffset + tileHeight, 0f);
            newDisc.transform.localPosition = position;

            newDisc.gameObject.isStatic = true;
            newDisc.gameObject.SetActive(true);
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
