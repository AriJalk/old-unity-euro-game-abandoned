using System.Collections.Generic;
using EDBG.Utilities.DataTypes;

namespace EDBG.Utilities
{
    public class BreadthFirstSearch
    {
        private bool[,] possibleMoves;
        private bool[,] visitedTiles;
        private int[,] distanceArray;
        private int maxDistance;
        private Queue<ICell> searchQueue;
        private GridContainer squareMap;

        public bool[,] Search(GridContainer map, ICell startCell, int maxDist)
        {
            squareMap = map;
            possibleMoves = new bool[map.Rows, map.Columns];
            visitedTiles = new bool[map.Rows, map.Columns];
            distanceArray = new int[map.Rows, map.Columns];

            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    distanceArray[i, j] = 0;
                }
            }

            maxDistance = maxDist;
            UpdatePossibleTiles(startCell);
            return possibleMoves;
        }

        private void UpdatePossibleTiles(ICell startTile)
        {
            searchQueue = new Queue<ICell>();
            searchQueue.Enqueue(startTile);
            distanceArray[startTile.GamePosition.Row, startTile.GamePosition.Col] = 0;
            BFS(startTile);
        }

        private void BFS(ICell startTile)
        {
            if (startTile == null)
                return;

            while (searchQueue.Count > 0)
            {
                ICell currentTile = searchQueue.Dequeue();
                visitedTiles[currentTile.GamePosition.Row, currentTile.GamePosition.Col] = true;

                if (distanceArray[currentTile.GamePosition.Row, currentTile.GamePosition.Col] <= maxDistance)
                {
                    possibleMoves[currentTile.GamePosition.Row, currentTile.GamePosition.Col] = true;
                }

                if (distanceArray[currentTile.GamePosition.Row, currentTile.GamePosition.Col] < maxDistance)
                {
                    EnqueueNeighborIfExists(currentTile, Direction.Up);
                    EnqueueNeighborIfExists(currentTile, Direction.Down);
                    EnqueueNeighborIfExists(currentTile, Direction.Left);
                    EnqueueNeighborIfExists(currentTile, Direction.Right);
                }
            }
        }

        private void EnqueueNeighborIfExists(ICell currentTile, Direction direction)
        {
            ICell neighbor = squareMap.GetNeighbor(currentTile, direction);
            if (neighbor != null && !visitedTiles[neighbor.GamePosition.Row, neighbor.GamePosition.Col])
            {
                distanceArray[neighbor.GamePosition.Row, neighbor.GamePosition.Col] = distanceArray[currentTile.GamePosition.Row, currentTile.GamePosition.Col] + 1;
                searchQueue.Enqueue(neighbor);
            }
        }
    }
}
