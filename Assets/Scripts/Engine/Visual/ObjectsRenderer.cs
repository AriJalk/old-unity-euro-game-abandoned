using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.Components;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.MapSystem;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using EDBG.Director;

namespace EDBG.Engine.Visual
{
    public class ObjectsRenderer
    {
        private ColorManager colorManager;
        private GameDirector director;

        public ObjectsRenderer()
        {
            colorManager = GameEngineManager.Instance.ColorManager;
        }

        public void SetDirector(GameDirector director)
        {
            this.director = director;
        }

        public void RenderObjectsOnMap(MapTileGameObject[,] tiles, bool isAnimated)
        {
            foreach (MapTileGameObject tile in tiles)
            {
                RenderObjectsOnTileObject(tile, isAnimated);
            }
        }

        public void RemoveTile(MapTileGameObject tile)
        {
            Transform stack = tile.Stack.transform;
            stack.localScale = Vector3.one;
            foreach (DiscObject disc in stack.GetComponentsInChildren<DiscObject>())
            {
                GameEngineManager.Instance.PrefabManager.ReturnPoolObject(disc);
            }
            GameEngineManager.Instance.PrefabManager.ReturnPoolObject(tile);
        }

        public void RenderObjectsOnTileObject(MapTileGameObject tile, bool isAnimated)
        {
            RemovePreviousDiscs(tile);
            tile.Stack.transform.localScale = Vector3.one;
            CreateNewDiscs(tile, isAnimated);
        }


        private void RemovePreviousDiscs(MapTileGameObject tile)
        {
            DiscObject[] discs = tile.Stack.GetComponentsInChildren<DiscObject>();
            foreach (DiscObject disc in discs)
            {
                GameEngineManager.Instance.PrefabManager.ReturnPoolObject<DiscObject>(disc);
            }
        }

        private void CreateNewDiscs(MapTileGameObject tile, bool isAnimated)
        {
            float discHeight = DiscObject.DISC_HEIGHT * DiscObject.DISC_SCALE;
            float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc
            float fillerDiscHeight = discHeight / DiscObject.FILLER_FACTOR;

            Transform parentStack = tile.Stack.transform;
            //float gridCellSize = MapTileGameObject.TILE_LENGTH / 3;
            if (tile.TileData.ComponentOnTile is GameStack<Disc> discStack && discStack.Count > 0)
            {
                for (int i = 0; i < discStack.Count; i++)
                {
                    // Create main disc
                    DiscObject mainDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                    mainDisc.name = "Disc";
                    mainDisc.DiscData = discStack.GetItemByIndex(i);
                    mainDisc.transform.SetParent(tile.Stack.transform);
                    mainDisc.transform.localScale = Vector3.one * DiscObject.DISC_SCALE;
                    Vector3 position = new Vector3(0, i * discHeight + initialHeightOffset + ((i != 0) ? fillerDiscHeight * i : 0), 0);
                    mainDisc.transform.localPosition = position;
                    // Apply Material based on disc color
                    mainDisc.ApplyMaterial(colorManager.GetDiscMaterial(mainDisc.DiscData.DiscColor));

                    if (isAnimated == true)
                    {
                        mainDisc.AnimatedObject.transform.localPosition = Vector3.one * 20f;
                        if (i == 0)
                            director.AddAnimationSimultanious(mainDisc.AnimatedObject, "PlaceDisc");
                    }
                    // Create filler disc
                    if (i < discStack.Count - 1)
                    {
                        DiscObject fillerDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                        fillerDisc.transform.SetParent(tile.Stack.transform);
                        fillerDisc.transform.localScale = new Vector3(DiscObject.DISC_SCALE, DiscObject.DISC_SCALE / DiscObject.FILLER_FACTOR, DiscObject.DISC_SCALE);
                        fillerDisc.transform.name = "Filler Disc";
                        float fillerYPos = position.y + discHeight;
                        fillerDisc.transform.localPosition = new Vector3(0, fillerYPos, 0);
                        fillerDisc.ApplyMaterial(colorManager.GetMaterial(
                            discStack.GetItemByIndex(i + 1).DiscColor == GameLogic.Rules.PlayerColors.White ? "BlackFiller" : "WhiteFiller"));
                        if (isAnimated == true)
                        {
                            fillerDisc.AnimatedObject.transform.localPosition = Vector3.one * 20f;
                            director.AddAnimationSimultanious(fillerDisc.AnimatedObject, "PlaceDisc");
                        }
                    }
                    if (i != 0 && isAnimated == true)
                        director.AddAnimationSimultanious(mainDisc.AnimatedObject, "PlaceDisc");

                }
            }
        }

        public void PlaceNewDisc(Disc disc, MapTile tileData, MapHolder mapHolder, bool isAnimated)
        {
            MapTileGameObject tileObject = mapHolder.GetTile(tileData.GamePosition);
            tileObject.TileData = tileData;
            DiscObject newDisc;
            float discHeight = DiscObject.DISC_HEIGHT * DiscObject.DISC_SCALE;
            float fillerDiscHeight = discHeight / DiscObject.FILLER_FACTOR;



            Transform stack = tileObject.Stack.transform;
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
                    DiscObject fillerDisc;
                    newFillerHeight = discHeight * discs + fillerDiscHeight * (discs - 1);
                    fillerDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                    fillerDisc.name = "Filler Disc";
                    fillerDisc.transform.SetParent(stack);
                    fillerDisc.transform.localScale = new Vector3(DiscObject.DISC_SCALE, DiscObject.DISC_SCALE / DiscObject.FILLER_FACTOR, DiscObject.DISC_SCALE);
                    fillerDisc.transform.localPosition = new Vector3(0, newFillerHeight, 0);

                    fillerDisc.ApplyMaterial(colorManager.GetMaterial(
                            disc.DiscColor ==
                            GameLogic.Rules.PlayerColors.White ? "BlackFiller" : "WhiteFiller"));
                    newDiscHeight = newFillerHeight + fillerDiscHeight;
                    if (isAnimated)
                    {
                        fillerDisc.AnimatedObject.transform.localPosition = Vector3.one * 20f;
                        director.AddAnimationToSequence(fillerDisc.AnimatedObject, "PlaceDisc");
                    }


                }

                //Add regular disc
                Vector3 position = new Vector3(0, newDiscHeight, 0);
                newDisc = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<DiscObject>();
                newDisc.DiscData = disc;
                newDisc.name = "Disc";
                newDisc.transform.SetParent(stack);
                newDisc.transform.localScale = Vector3.one * DiscObject.DISC_SCALE;
                newDisc.transform.localPosition = position;
                // Apply Material based on disc color
                newDisc.ApplyMaterial(colorManager.GetDiscMaterial(newDisc.DiscData.DiscColor));
                if (isAnimated)
                {
                    newDisc.AnimatedObject.transform.localPosition = Vector3.one * 20f;
                    director.AddAnimationToSequence(newDisc.AnimatedObject, "PlaceDisc");
                }
            }
        }


    }
}