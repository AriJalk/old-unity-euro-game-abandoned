using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.Components;
using EDBG.Engine.ResourceManagement;

namespace EDBG.Engine.Visual
{
    public class ObjectsRenderer : MonoBehaviour
    {
        private ColorManager colorManager;
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
            colorManager = GameEngineManager.Instance.ColorManager;
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
                GameEngineManager.Instance.PrefabManager.ReturnPoolObject(nameof(DiscObject), disc.gameObject);
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
                    DiscObject newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject(nameof(DiscObject)).GetComponent<DiscObject>();
                    newDisc.enabled = true;
                    newDisc.discData = discStack.GetItemByIndex(i);
                    newDisc.transform.SetParent(tile.transform);
                    newDisc.transform.localScale = Vector3.one * DiscScale;
                    Vector3 position = new Vector3(0, i * discHeight + initialHeightOffset + tileHeight + ((i != 0) ? fillerDiscHeight * i : 0), 0);
                    newDisc.transform.localPosition = position;

                    // Apply Material based on disc color
                    newDisc.ApplyMaterial(colorManager.GetDiscMaterial(newDisc.discData.DiscColor));
                    //Create filler disc
                    if (i < discStack.Count - 1)
                    {
                        newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject(nameof(DiscObject)).GetComponent<DiscObject>();
                        newDisc.transform.SetParent(tile.transform);
                        newDisc.transform.localScale = new Vector3(DiscScale, DiscScale / fillerDiscFactor, DiscScale);
                        float fillerYPos = position.y + discHeight;
                        newDisc.transform.localPosition = new Vector3(0, fillerYPos, 0);
                        newDisc.ApplyMaterial(colorManager.GetDiscMaterial(discStack.GetItemByIndex(i).DiscColor));
                    }
                }
            }
        }
    }
}