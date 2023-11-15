using UnityEngine;

using EDBG.Engine.Core;
using EDBG.GameLogic.MapSystem;
using System;
using Unity.VisualScripting;
using EDBG.GameLogic.Rules;

namespace EDBG.Engine.Visual
{
    public class MapRenderer : MonoBehaviour
    {
        //TODO: move to SquareMapHolder
        private SquareTileObject[,] tiles;


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

        public void RenderMap(MapGrid map, Transform mapHolderObject)
        {
            tiles = new SquareTileObject[map.Rows, map.Columns];
            RemovePreviousTiles(mapHolderObject);
            for (int row = 0; row < map.Rows; row++)
            {
                for (int col = 0; col < map.Columns; col++)
                {
                    SquareTileObject tile = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<SquareTileObject>();
                    tile.TileData = (MapTile)map.GetCell(row, col);
                    tile.transform.SetParent(mapHolderObject);
                    tile.transform.localScale = Vector3.one;
                    Vector3 position = new Vector3(row * (SquareTileObject.TILE_SPACING + SquareTileObject.TILE_LENGTH)
                        , 0.1f
                        , col * (SquareTileObject.TILE_SPACING + SquareTileObject.TILE_LENGTH));
                    tile.transform.localPosition = position;
                    tile.gameObject.isStatic = true;
                    tile.gameObject.SetActive(true);
                    tile.name = "Tile [" + col + "," + row + "]";
                    tile.ApplyMaterial(ColorManager.Instance.GetTileMaterial(tile.TileData.TileColor));
                    tiles[row, col] = tile;
                    DrawDieFace(tile);
                    GameEngineManager.Instance.ObjectsRenderer.RenderObjectsOnTileObject(tile);
                }
            }
        }

        public SquareTileObject GetTileObject(int row, int col)
        {
            return tiles[row, col];
        }

        public void SetTileObjectAndRender(MapTile tile)
        {
            if (tile != null)
            {
                SquareTileObject squareTileObject = GameEngineManager.Instance.PrefabManager.RetrievePoolObject<SquareTileObject>();
                squareTileObject.TileData = tile;

            }
            else
                Debug.LogError("Cant render tile, tile is NULL");
        }

        private void RemovePreviousTiles(Transform grid)
        {
            SquareTileObject[] tiles = grid.GetComponentsInChildren<SquareTileObject>();
            foreach (SquareTileObject tile in tiles)
            {
                GameEngineManager.Instance.PrefabManager.ReturnPoolObject<SquareTileObject>(tile);
            }
        }

        private void DrawDieFace(SquareTileObject tile)
        {
            SpriteRenderer spriteRenderer = tile.transform.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = GameEngineManager.Instance.SpriteManager.LoadSprite($"DieFaces/{tile.TileData.DieFace}");

        }
    }
}