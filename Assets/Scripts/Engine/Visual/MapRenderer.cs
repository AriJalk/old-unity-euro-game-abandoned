using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.MapSystem;
using EDBG.Director;

namespace EDBG.Engine.Visual
{
    public class MapRenderer
    {
        //TODO: move to SquareMapHolder
        private MapTileGameObject[,] tiles;
        private VisualManager visualManager;

        public MapRenderer(VisualManager visualManager)
        {
            this.visualManager = visualManager;
        }

        public void RenderMap(MapGrid map, bool isAnimated)
        {
            tiles = new MapTileGameObject[map.Rows, map.Columns];
            RemovePreviousTiles(visualManager.MapHolder);
            for (int row = 0; row < map.Rows; row++)
            {
                for (int col = 0; col < map.Columns; col++)
                {
                    MapTileGameObject tile = visualManager.ResourcesManager.PrefabManager.RetrievePoolObject<MapTileGameObject>();
                    tile.TileData = (MapTile)map.GetCell(row, col);
                    tile.transform.SetParent(visualManager.MapHolder.transform);
                    tile.transform.localScale = Vector3.one;
                    Vector3 position = new Vector3(row * (MapTileGameObject.TILE_SPACING + MapTileGameObject.TILE_LENGTH)
                        , 0.1f
                        , col * (MapTileGameObject.TILE_SPACING + MapTileGameObject.TILE_LENGTH));
                    tile.transform.localPosition = position;
                    tile.gameObject.isStatic = true;
                    tile.name = tile.TileData.ToString();
                    tile.ApplyMaterial(visualManager.ColorManager.GetTileMaterial(tile.TileData.TileColor));
                    tiles[row, col] = tile;
                    DrawDieFace(tile);
                    visualManager.ObjectsRenderer.RenderObjectsOnTileObject(tile, isAnimated);
                }
            }
            visualManager.MapHolder.SetTiles(tiles);
        }



        public MapTileGameObject GetTileObject(int row, int col)
        {
            return tiles[row, col];
        }

        public void SetTileObjectAndRender(MapTile tile)
        {
            if (tile != null)
            {
                MapTileGameObject squareTileObject = visualManager.ResourcesManager.PrefabManager.RetrievePoolObject<MapTileGameObject>();
                squareTileObject.TileData = tile;

            }
            else
                Debug.LogError("Cant render tile, tile is NULL");
        }

        private void RemovePreviousTiles(MapHolder grid)
        {
            MapTileGameObject[] tiles = grid.GetComponentsInChildren<MapTileGameObject>();
            foreach (MapTileGameObject tile in tiles)
            {
                visualManager.ObjectsRenderer.RemoveTile(tile);
            }
        }

        private void DrawDieFace(MapTileGameObject tile)
        {
            SpriteRenderer spriteRenderer = tile.transform.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = visualManager.ResourcesManager.SpriteManager.LoadSprite($"DieFaces/{tile.TileData.DieFace}");

        }
    }
}