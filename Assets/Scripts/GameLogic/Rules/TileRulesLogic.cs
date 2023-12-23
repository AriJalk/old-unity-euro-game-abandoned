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

        public static List<ICell> GetCellsInDirectLine(GridContainer map, ICell originCell)
        {
            List<ICell> cells = new List<ICell>();

            // Get cells in column
            for (int i = 0; i < map.Rows; i++)
            {
                if (i != originCell.GamePosition.Row)
                    cells.Add(map.GetCell(i, originCell.GamePosition.Col));
            }

            // Get cells in row
            for (int i = 0; i < map.Columns; i++)
            {
                if (i != originCell.GamePosition.Col)
                    cells.Add(map.GetCell(originCell.GamePosition.Row, i));
            }

            // Test
            Debug.Log("Direct Line Test");
            foreach (ICell cellx in cells)
                Debug.Log(cellx.GamePosition);

            return cells;
        }

        private static bool IsNextFloorPossible(GridContainer map, MapTile originTile, Player player)
        {
            List<MapTile> tiles = GetCellsInDirectLine(map, originTile).Cast<MapTile>().ToList();
            byte tilesWithPlayerDiscs = 0;
            foreach (MapTile tile in tiles)
            {
                if (tile.ComponentOnTile != null && tile.ComponentOnTile.Owner == player)
                    tilesWithPlayerDiscs++;

            }
            // 2nd floor requires 1 additional disc from connected tile, 3 requires 2 etc
            if (tilesWithPlayerDiscs < ((GameStack<Disc>)originTile.ComponentOnTile).Count)
                return false;
            return true;
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
    }
}
