using System.Collections.Generic;
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
                for(int j=0; j < map.Columns; j++)
                {
                    mapString += distanceMap[i, j] ? "X" : "O";
                }
                mapString += "\n";
            }
            Debug.Log(mapString);
            return distanceMap;
        }

        public static List<ICell> GetCellsInDirectLine(GridContainer map, ICell cell)
        {
            List<ICell> cells = new List<ICell>();

            // Get cells in column
            for (int i = 0; i < map.Rows; i++)
            {
                if (i != cell.GamePosition.Row)
                    cells.Add(map.GetCell(i, cell.GamePosition.Col));
            }

            // Get cells in row
            for (int i = 0; i < map.Columns; i++)
            {
                if (i != cell.GamePosition.Col)
                    cells.Add(map.GetCell(cell.GamePosition.Row, i));
            }

            // Test
            Debug.Log("Direct Line Test");
            foreach (ICell cellx in cells)
                Debug.Log(cellx.GamePosition);

            return cells;
        }
    }
}
