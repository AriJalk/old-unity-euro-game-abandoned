using System.Collections.Generic;
using EDBG.Utilities;
using EDBG.Utilities.DataTypes;
using UnityEngine;

namespace EDBG.GameLogic.Rules
{
    // TODO: Rename class, singleton
    public class TileRulesLogic
    {
        public static bool[,] GetTileInDistance(GridContainer map, ICell cell, int distance)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch();
            return bfs.Search(map, cell, distance);
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

            foreach (ICell cellx in cells)
                Debug.Log(cellx.GamePosition);

            return cells;
        }
    }
}
