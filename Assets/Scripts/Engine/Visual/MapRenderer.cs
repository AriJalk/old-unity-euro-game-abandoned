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
        private GameDirector director;

        public void SetDirector(GameDirector director)
        {
            this.director = director;
        }

        public void RenderMap(MapGrid map, MapHolder mapHolderObject, bool isAnimated, EngineManagerScpritableObject engineManager)
        {
            tiles = new MapTileGameObject[map.Rows, map.Columns];
            RemovePreviousTiles(mapHolderObject, engineManager);
            for (int row = 0; row < map.Rows; row++)
            {
                for (int col = 0; col < map.Columns; col++)
                {
                    MapTileGameObject tile = engineManager.PrefabManager.RetrievePoolObject<MapTileGameObject>();
                    tile.TileData = (MapTile)map.GetCell(row, col);
                    tile.transform.SetParent(mapHolderObject.transform);
                    tile.transform.localScale = Vector3.one;
                    Vector3 position = new Vector3(row * (MapTileGameObject.TILE_SPACING + MapTileGameObject.TILE_LENGTH)
                        , 0.1f
                        , col * (MapTileGameObject.TILE_SPACING + MapTileGameObject.TILE_LENGTH));
                    tile.transform.localPosition = position;
                    tile.gameObject.isStatic = true;
                    tile.name = tile.TileData.ToString();
                    tile.ApplyMaterial(engineManager.ColorManager.GetTileMaterial(tile.TileData.TileColor));
                    tiles[row, col] = tile;
                    DrawDieFace(tile, engineManager);
                    engineManager.ObjectsRenderer.RenderObjectsOnTileObject(tile, isAnimated, engineManager);
                }
            }
            mapHolderObject.SetTiles(tiles);
        }



        public MapTileGameObject GetTileObject(int row, int col)
        {
            return tiles[row, col];
        }

        public void SetTileObjectAndRender(MapTile tile, EngineManagerScpritableObject engineManager)
        {
            if (tile != null)
            {
                MapTileGameObject squareTileObject = engineManager.PrefabManager.RetrievePoolObject<MapTileGameObject>();
                squareTileObject.TileData = tile;

            }
            else
                Debug.LogError("Cant render tile, tile is NULL");
        }

        private void RemovePreviousTiles(MapHolder grid, EngineManagerScpritableObject engineManager)
        {
            MapTileGameObject[] tiles = grid.GetComponentsInChildren<MapTileGameObject>();
            foreach (MapTileGameObject tile in tiles)
            {
                engineManager.ObjectsRenderer.RemoveTile(tile, engineManager);
            }
        }

        private void DrawDieFace(MapTileGameObject tile, EngineManagerScpritableObject engineManager)
        {
            SpriteRenderer spriteRenderer = tile.transform.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = engineManager.SpriteManager.LoadSprite($"DieFaces/{tile.TileData.DieFace}");

        }
    }
}