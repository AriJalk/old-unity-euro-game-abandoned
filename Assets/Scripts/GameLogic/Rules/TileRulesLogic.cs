using System.Collections.Generic;
using System.Linq;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.States;
using EDBG.Utilities;
using EDBG.Utilities.DataTypes;
using UnityEngine;

namespace EDBG.GameLogic.Rules
{
    // TODO: Rename class, singleton
    public class TileRulesLogic
    {

        public static bool IsNextFloorPossible(GridContainer map, MapTile originTile, Player player)
        {
            List<ICell> tiles = GetCellsWithComponentInAllDirections(map, originTile, player, true, true);
            int tilesWithPlayerDiscs = tiles.Count;

            // 2nd floor requires 1 additional disc from connected tile, 3 requires 2 etc
            if (tilesWithPlayerDiscs < ((GameStack<Disc>)originTile.ComponentOnTile).Count + 1)
                return false;
            return true;
        }

        public static List<ICell> GetTilesThatContribute(GridContainer map, MapTile originTile, Player player)
        {

            List<ICell> tiles = GetCellsWithComponentInAllDirections(map, originTile, player, true, true);
            int tilesWithPlayerDiscs = tiles.Count;

            return tiles;
        }

        private static List<ICell> GetCellsWithComponentInDirection(GridContainer map, ICell originCell, Player player, bool canBlock, bool isPlayerOwned, Direction direction)
        {
            MapTile tile = originCell as MapTile;
            List<ICell> cells = new List<ICell>();
            while (map.GetNeighbor(tile, direction) != null)
            {
                tile = map.GetNeighbor(tile, direction) as MapTile;
                if (tile.ComponentOnTile != null)
                {
                    if ((tile.ComponentOnTile.Owner == player || tile.ComponentOnTile.Owner == null) ||
                        (tile.ComponentOnTile.Owner != null && tile.ComponentOnTile.Owner != player && !isPlayerOwned))
                    {
                        cells.Add(tile);
                        if (canBlock)
                            break;
                    }
                }
            }
            return cells;
        }

        private static List<ICell> GetCellsWithComponentInAllDirections(GridContainer map, ICell originCell, Player player, bool canBlock, bool isPlayerOwned)
        {
            List<ICell> cells = GetCellsWithComponentInDirection(map, originCell, player, canBlock, isPlayerOwned, Direction.Left);
            cells.AddRange(GetCellsWithComponentInDirection(map, originCell, player, canBlock, isPlayerOwned, Direction.Right));
            cells.AddRange(GetCellsWithComponentInDirection(map, originCell, player, canBlock, isPlayerOwned, Direction.Up));
            cells.AddRange(GetCellsWithComponentInDirection(map, originCell, player, canBlock, isPlayerOwned, Direction.Down));

            return cells;
        }


        /// <summary>
        /// Receives a grid container and a cell, return 2d array of true where in distance to cell
        /// </summary>
        /// <param name="map"></param>
        /// <param name="cell"></param>
        /// <param name="distance"></param>
        /// <returns>
        /// True for cells in distance
        /// </returns>
        public static bool[,] GetCellsInDistance(GridContainer map, ICell cell, int distance)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch();
            bool[,] distanceMap = bfs.Search(map, cell, distance);
            Debug.Log("Bfs Test Start: ");
            string mapString = string.Empty;
            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    mapString += distanceMap[i, j] ? "X" : "O";
                }
                mapString += "\n";
            }
            Debug.Log(mapString);
            return distanceMap;
        }

        public static List<ICell> GetCellsInDirectLine(GridContainer map, ICell originCell, bool canBlock)
        {
            List<ICell> cells = new List<ICell>();

            // Get cells in column
            for (int i = 0; i < map.Rows; i++)
            {
                if (i != originCell.GamePosition.Row)
                {
                    cells.Add(map.GetCell(i, originCell.GamePosition.Col));
                }
            }

            // Get cells in row
            for (int i = 0; i < map.Columns; i++)
            {
                if (i != originCell.GamePosition.Col)
                {
                    cells.Add(map.GetCell(originCell.GamePosition.Row, i));
                }
            }

            // Test
            Debug.Log("Direct Line Test");
            foreach (ICell cellx in cells)
                Debug.Log(cellx.GamePosition);

            return cells;
        }

        public static bool IsTileValid(ChooseTile chooseTile)
        {
            if (chooseTile.SelectedTile.ComponentOnTile == null)
            {
                return true;
            }
            else if (
                chooseTile.SelectedTile.ComponentOnTile.Owner == chooseTile.LogicState.GetCurrentPlayer() ||
                chooseTile.SelectedTile.ComponentOnTile.Owner == null)
            {
                return TileRulesLogic.IsNextFloorPossible(
                    chooseTile.LogicState.MapGrid,
                    chooseTile.SelectedTile,
                    chooseTile.LogicState.GetCurrentPlayer());
            }
            else
            {
                return false;
            }
        }

        public static void AddDiscToTile(ChooseTile chooseTile)
        {
            if (chooseTile.SelectedTile.ComponentOnTile == null)
                chooseTile.SelectedTile.ComponentOnTile = new GameStack<Disc>();
            ((GameStack<Disc>)chooseTile.SelectedTile.ComponentOnTile).PushItem(new Disc(chooseTile.LogicState.GetCurrentPlayer()));
        }

        public static List<MapTile> GetBiggerOpponentStackTiles(ChooseTile chosenTile)
        {
            GameStack<Disc> originStack = chosenTile.SelectedTile.ComponentOnTile as GameStack<Disc>;
            if (originStack == null)
                return null;
            int stackSize = originStack.Count;
            Player player = chosenTile.LogicState.GetCurrentPlayer();
            
            List<ICell> cells = GetCellsWithComponentInAllDirections(chosenTile.LogicState.MapGrid, chosenTile.SelectedTile, player, true, true);
            List<MapTile> tiles = new List<MapTile>();
            foreach(ICell cell in cells)
            {
                MapTile tile = cell as MapTile;
                if (tile != null)
                {
                    GameStack<Disc> stack = tile.ComponentOnTile as GameStack<Disc>;
                    if (stack.Count > stackSize)
                        tiles.Add(tile);
                }
            }
            return tiles;
        }
    }
}
