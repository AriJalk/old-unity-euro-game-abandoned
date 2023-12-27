using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.Components;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.MapSystem;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

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

        public void RenderObjectsOnMap(MapTileGameObject[,] tiles)
        {
            foreach (MapTileGameObject tile in tiles)
            {
                RenderObjectsOnTileObject(tile);
            }
        }

        public void RemoveTile(MapTileGameObject tile)
        {
            Transform stack = tile.Stack;
            foreach (DiscObject disc in stack.GetComponentsInChildren<DiscObject>())
            {
                GameEngineManager.Instance.PrefabManager.ReturnPoolObject(disc);
            }
            GameEngineManager.Instance.PrefabManager.ReturnPoolObject(tile);
        }

        public void RenderObjectsOnTileObject(MapTileGameObject tile)
        {
            RemovePreviousDiscs(tile);
            tile.Stack.localScale = Vector3.one;
            CreateNewDiscs(tile);
        }


        private void RemovePreviousDiscs(MapTileGameObject tile)
        {
            DiscObject[] discs = tile.Stack.GetComponentsInChildren<DiscObject>();
            foreach (DiscObject disc in discs)
            {
                GameEngineManager.Instance.PrefabManager.ReturnPoolObject<DiscObject>(disc);
            }
        }

        private void CreateNewDiscs(MapTileGameObject tile)
        {
            float discHeight = DiscObject.DISC_HEIGHT * DiscScale;
            float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc
            float fillerDiscFactor = 6;
            float fillerDiscHeight = discHeight / fillerDiscFactor;

            Transform parentStack = tile.Stack;
            //float gridCellSize = MapTileGameObject.TILE_LENGTH / 3;
            if (tile.TileData.ComponentOnTile is GameStack<Disc> discStack && discStack.Count > 0)
            {
                for (int i = 0; i < discStack.Count; i++)
                {

                    DiscObject newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                    newDisc.name = "Disc";
                    newDisc.discData = discStack.GetItemByIndex(i);
                    newDisc.transform.SetParent(tile.Stack);
                    newDisc.transform.localScale = Vector3.one * DiscScale;
                    Vector3 position = new Vector3(0, i * discHeight + initialHeightOffset + ((i != 0) ? fillerDiscHeight * i : 0), 0);
                    newDisc.transform.localPosition = position;

                    // Apply Material based on disc color
                    newDisc.ApplyMaterial(colorManager.GetDiscMaterial(newDisc.discData.DiscColor));
                    //Create filler disc
                    if (i < discStack.Count - 1)
                    {
                        newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                        newDisc.transform.SetParent(tile.Stack);
                        newDisc.transform.localScale = new Vector3(DiscScale, DiscScale / fillerDiscFactor, DiscScale);
                        newDisc.transform.name = "Filler Disc";
                        float fillerYPos = position.y + discHeight;
                        newDisc.transform.localPosition = new Vector3(0, fillerYPos, 0);
                        newDisc.ApplyMaterial(colorManager.GetMaterial(
                            discStack.GetItemByIndex(i).DiscColor ==
                            GameLogic.Rules.PlayerColors.White ? "BlackFiller" : "WhiteFiller"));
                    }
                }
            }
        }

        public void PlaceNewDisc(Disc disc, MapTile tileData, SquareMapHolderObject mapHolder)
        {
            MapTileGameObject tileObject = mapHolder.GetTile(tileData.GamePosition);
            tileObject.TileData = tileData;
            DiscObject newDisc;
            float discHeight = DiscObject.DISC_HEIGHT * DiscScale;
            float fillerDiscFactor = 6;
            float fillerDiscHeight = discHeight / fillerDiscFactor;



            Transform stack = tileObject.Stack;
            if (stack != null)
            {
                byte discs = 0;
                float newFillerHeight = 0f;
                float newDiscHeight = 0f;

                foreach (Transform child in stack)
                {
                    if (child.name == "Disc")
                        discs++;
                }
                //Add filler if needed
                if (discs > 0)
                {
                    newFillerHeight = discHeight * discs + fillerDiscHeight * (discs - 1);
                    newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                    newDisc.name = "Filler Disc";
                    newDisc.transform.SetParent(stack);
                    newDisc.transform.localScale = new Vector3(DiscScale, DiscScale / fillerDiscFactor, DiscScale);
                    newDisc.transform.localPosition = new Vector3(0, newFillerHeight, 0);

                    newDisc.ApplyMaterial(colorManager.GetMaterial(
                            disc.DiscColor ==
                            GameLogic.Rules.PlayerColors.White ? "BlackFiller" : "WhiteFiller"));
                    newDiscHeight = newFillerHeight + fillerDiscHeight;

                }
                //Add regular disc
                Vector3 position = new Vector3(0, newDiscHeight, 0);
                newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                newDisc.discData = disc;
                newDisc.name = "Disc";
                newDisc.transform.SetParent(stack);
                newDisc.transform.localScale = Vector3.one * DiscScale;
                newDisc.transform.localPosition = position;
                // Apply Material based on disc color
                newDisc.ApplyMaterial(colorManager.GetDiscMaterial(newDisc.discData.DiscColor));
            }
        }

        public void PlaceDiscAnimated(Disc disc, MapTileGameObject tile)
        {
            int height = tile.TileData.GetComponentOnTile<GameStack<Disc>>().Count;

        }


    }
}