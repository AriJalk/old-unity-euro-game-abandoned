using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.Components;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.MapSystem;
using EDBG.Director;
using Unity.VisualScripting;

namespace EDBG.Engine.Visual
{
    public class ObjectsRenderer
    {
        private VisualManager visualManager;

        public ObjectsRenderer(VisualManager visualManager)
        {
            this.visualManager = visualManager;
        }

        //Adds animation to component and registers it in the animation manager
        private void AddAnimation<T>(GameObject gameObject) where T : CodeAnimationBase
        {
            CodeAnimationBase anim = gameObject.AddComponent<T>();
            anim.SetAnimationManager(visualManager.AnimationManager);
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
            Transform stack = tile.StackContainer.Find("Stack").transform;
            stack.localScale = Vector3.one;
            foreach (DiscObject disc in stack.GetComponentsInChildren<DiscObject>())
            {
                visualManager.ResourcesManager.PrefabManager.ReturnPoolObject(disc);
            }
            visualManager.ResourcesManager.PrefabManager.ReturnPoolObject(tile);
        }

        public void RenderObjectsOnTileObject(MapTileGameObject tile, bool isAnimated)
        {
            RemovePreviousDiscs(tile);
            tile.StackContainer.Find("Stack").transform.localScale = Vector3.one;
            CreateNewDiscs(tile, isAnimated);
        }


        private void RemovePreviousDiscs(MapTileGameObject tile)
        {
            DiscObject[] discs = tile.StackContainer.Find("Stack").GetComponentsInChildren<DiscObject>();
            foreach (DiscObject disc in discs)
            {
                visualManager.ResourcesManager.PrefabManager.ReturnPoolObject<DiscObject>(disc);
            }
        }

        private void CreateNewDiscs(MapTileGameObject tile, bool isAnimated)
        {
            float discHeight = DiscObject.DISC_HEIGHT * DiscObject.DISC_SCALE;
            float initialHeightOffset = 0.0f; // Adjust this value to control the initial height offset of the first disc
            float fillerDiscHeight = discHeight / DiscObject.FILLER_FACTOR;

            Transform parentStack = tile.StackContainer.Find("Stack").transform;
            //float gridCellSize = MapTileGameObject.TILE_LENGTH / 3;
            if (tile.TileData.ComponentOnTile is GameStack<Disc> discStack && discStack.Count > 0)
            {
                for (int i = 0; i < discStack.Count; i++)
                {
                    // Create main disc
                    DiscObject mainDisc = visualManager.ResourcesManager.PrefabManager.RetrievePoolObject<DiscObject>();
                    mainDisc.name = "Disc";
                    mainDisc.DiscData = discStack.GetItemByIndex(i);
                    mainDisc.transform.SetParent(tile.StackContainer.Find("Stack").transform);
                    mainDisc.transform.localScale = Vector3.one * DiscObject.DISC_SCALE;
                    Vector3 position = new Vector3(0, i * discHeight + initialHeightOffset + ((i != 0) ? fillerDiscHeight * i : 0), 0);
                    mainDisc.transform.localPosition = position;
                    // Apply Material based on disc color
                    mainDisc.ApplyMaterial(visualManager.ColorManager.GetDiscMaterial(mainDisc.DiscData.Owner.PlayerColor));

                    if (isAnimated == true)
                    {
                        if (i == 0)
                            AddAnimation<PlaceDiscAnimation>(mainDisc.gameObject);
                    }
                    // Create filler disc
                    if (i < discStack.Count - 1)
                    {
                        DiscObject fillerDisc = visualManager.ResourcesManager.PrefabManager.RetrievePoolObject<DiscObject>();
                        fillerDisc.transform.SetParent(tile.StackContainer.Find("Stack").transform);
                        fillerDisc.transform.localScale = new Vector3(DiscObject.DISC_SCALE, DiscObject.DISC_SCALE / DiscObject.FILLER_FACTOR, DiscObject.DISC_SCALE);
                        fillerDisc.transform.name = "Filler Disc";
                        float fillerYPos = position.y + discHeight;
                        fillerDisc.transform.localPosition = new Vector3(0, fillerYPos, 0);
                        fillerDisc.ApplyMaterial(visualManager.ColorManager.GetMaterial(
                            discStack.GetItemByIndex(i + 1).Owner.PlayerColor == GameLogic.Rules.PlayerColors.White ? "BlackFiller" : "WhiteFiller"));
                        if (isAnimated == true)
                        {
                            AddAnimation<PlaceDiscAnimation>(fillerDisc.gameObject);
                            //director.AddAnimationSimultanious(fillerDisc.AnimatedObject, "PlaceDisc");
                        }
                    }
                    if (i != 0 && isAnimated == true)
                        AddAnimation<PlaceDiscAnimation>(mainDisc.gameObject);
                    //director.AddAnimationSimultanious(mainDisc.AnimatedObject, "PlaceDisc");

                }
            }
        }

        public void PlaceNewDisc(Disc disc, MapTile tileData, bool isAnimated)
        {
            MapTileGameObject tileObject = visualManager.MapHolder.GetTile(tileData.GamePosition);
            tileObject.TileData = tileData;
            DiscObject newDisc;
            float discHeight = DiscObject.DISC_HEIGHT * DiscObject.DISC_SCALE;
            float fillerDiscHeight = discHeight / DiscObject.FILLER_FACTOR;



            Transform stack = tileObject.StackContainer.Find("Stack");
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
                    fillerDisc = visualManager.ResourcesManager.PrefabManager.RetrievePoolObject<DiscObject>();
                    fillerDisc.name = "Filler Disc";
                    fillerDisc.transform.SetParent(stack);
                    fillerDisc.transform.localScale = new Vector3(DiscObject.DISC_SCALE, DiscObject.DISC_SCALE / DiscObject.FILLER_FACTOR, DiscObject.DISC_SCALE);
                    fillerDisc.transform.localPosition = new Vector3(0, newFillerHeight, 0);

                    fillerDisc.ApplyMaterial(visualManager.ColorManager.GetMaterial(
                            disc.Owner.PlayerColor ==
                            GameLogic.Rules.PlayerColors.White ? "BlackFiller" : "WhiteFiller"));
                    newDiscHeight = newFillerHeight + fillerDiscHeight;
                    if (isAnimated)
                    {
                        AddAnimation<PlaceDiscAnimation>(fillerDisc.gameObject);
                    }
                }
                //Add regular disc
                Vector3 position = new Vector3(0, newDiscHeight, 0);
                newDisc = visualManager.ResourcesManager.PrefabManager.RetrievePoolObject<DiscObject>();
                newDisc.DiscData = disc;
                newDisc.name = "Disc";
                newDisc.transform.SetParent(stack);
                newDisc.transform.localScale = Vector3.one * DiscObject.DISC_SCALE;
                newDisc.transform.localPosition = position;
                // Apply Material based on disc color
                newDisc.ApplyMaterial(visualManager.ColorManager.GetDiscMaterial(newDisc.DiscData.Owner.PlayerColor));
                if (isAnimated)
                {
                    AddAnimation<PlaceDiscAnimation>(newDisc.gameObject);
                }
            }
        }

        public void RemoveTopDisc(MapTile mapTile)
        {

            Transform stack = visualManager.MapHolder.GetTile(mapTile.GamePosition).StackContainer.GetChild(0);
            if (stack != null)
            {
                int childCount = stack.childCount;
                if (childCount > 0)
                {
                    //Remove filler disc if there are more than 1 disc
                    if (childCount > 1)
                    {
                        visualManager.ResourcesManager.PrefabManager.ReturnPoolObject(stack.GetChild(childCount - 1).GetComponent<DiscObject>());
                        childCount--;
                    }
                    //Remove disc
                    visualManager.ResourcesManager.PrefabManager.ReturnPoolObject(stack.GetChild(childCount - 1).GetComponent<DiscObject>());
                    childCount--;
                }

            }
        }
    }
}