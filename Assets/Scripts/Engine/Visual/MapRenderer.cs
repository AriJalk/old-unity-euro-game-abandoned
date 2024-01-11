using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.MapSystem;
using System;
using Unity.VisualScripting;
using EDBG.GameLogic.Rules;

namespace EDBG.Engine.Visual
{
    public class MapRenderer
    {
        //TODO: move to SquareMapHolder
        private MapTileGameObject[,] tiles;

        public void RenderMap(MapGrid map, SquareMapHolderObject mapHolderObject, bool isAnimated)
        {
            tiles = new MapTileGameObject[map.Rows, map.Columns];
            RemovePreviousTiles(mapHolderObject);
            for (int row = 0; row < map.Rows; row++)
            {
                for (int col = 0; col < map.Columns; col++)
                {
                    MapTileGameObject tile = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<MapTileGameObject>();
                    tile.TileData = (MapTile)map.GetCell(row, col);
                    tile.transform.SetParent(mapHolderObject.transform);
                    tile.transform.localScale = Vector3.one;
                    Vector3 position = new Vector3(row * (MapTileGameObject.TILE_SPACING + MapTileGameObject.TILE_LENGTH)
                        , 0.1f
                        , col * (MapTileGameObject.TILE_SPACING + MapTileGameObject.TILE_LENGTH));
                    tile.transform.localPosition = position;
                    tile.gameObject.isStatic = true;
                    tile.gameObject.SetActive(true);
                    tile.name = tile.TileData.ToString();
                    tile.ApplyMaterial(ColorManager.Instance.GetTileMaterial(tile.TileData.TileColor));
                    tiles[row, col] = tile;
                    DrawDieFace(tile);
                    GameEngineManager.Instance.ObjectsRenderer.RenderObjectsOnTileObject(tile, isAnimated);
                }
            }
            mapHolderObject.SetTiles(tiles);
        }



        public MapTileGameObject GetTileObject(int row, int col)
        {
            return tiles[row, col];
        }

        public void SetTileObjectAndRender(MapTile tile)
        {
            if (tile != null)
            {
                MapTileGameObject squareTileObject = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<MapTileGameObject>();
                squareTileObject.TileData = tile;

            }
            else
                Debug.LogError("Cant render tile, tile is NULL");
        }

        private void RemovePreviousTiles(SquareMapHolderObject grid)
        {
            MapTileGameObject[] tiles = grid.GetComponentsInChildren<MapTileGameObject>();
            foreach (MapTileGameObject tile in tiles)
            {
                GameEngineManager.Instance.ObjectsRenderer.RemoveTile(tile);
            }
        }

        private void DrawDieFace(MapTileGameObject tile)
        {
            SpriteRenderer spriteRenderer = tile.transform.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = GameEngineManager.Instance.SpriteManager.LoadSprite($"DieFaces/{tile.TileData.DieFace}");

        }
    }
}