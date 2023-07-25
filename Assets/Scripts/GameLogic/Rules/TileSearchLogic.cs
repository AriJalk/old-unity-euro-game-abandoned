using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using Unity.VisualScripting;
using System.Linq;

namespace EDBG.Rules
{
    public class TileRulesLogic
    {
        static int gridRows;
        static int gridColumns;
        static bool[,] possibleMoves;
        static bool[,] visitedTiles;
        static int[,] distanceArray;
        static int maxDistance;
        static Queue<MapTile> searchQueue;
        static MapGrid squareMap;

        public static void Initialize(int rows, int columns)
        {
            gridRows = rows;
            gridColumns = columns;
        }

        public static bool[,] GetPossibleMoves(MapGrid map, MapTile tile, int distance)
        {
            squareMap = map;
            possibleMoves = new bool[gridRows, gridColumns];
            visitedTiles = new bool[gridRows, gridColumns];
            distanceArray = new int[gridRows, gridColumns];
            for (int i = 0; i < squareMap.Rows; i++)
            {
                for (int j = 0; j < squareMap.Columns; j++)
                {
                    distanceArray[i, j] = 0;
                }
            }
            maxDistance = distance;
            UpdatePossibleTiles(squareMap.GetTile(tile.GamePosition.X, tile.GamePosition.Y));
            return possibleMoves;
        }

        static void UpdatePossibleTiles(MapTile tile)
        {
            searchQueue = new Queue<MapTile>();
            searchQueue.Enqueue(tile);
            distanceArray[tile.GamePosition.X, tile.GamePosition.Y] = 0;
            BFS(tile);
        }

        static void BFS(MapTile tile)
        {
            if (tile == null)
                return;

            while (searchQueue.Count > 0)
            {
                MapTile upNeighbor;
                MapTile downNeighbor;
                MapTile leftNeighbor;
                MapTile rightNeighbor;
                tile = searchQueue.Dequeue();
                visitedTiles[tile.GamePosition.X, tile.GamePosition.Y] = true;
                if (distanceArray[tile.GamePosition.X, tile.GamePosition.Y] <= maxDistance)
                {
                    possibleMoves[tile.GamePosition.X, tile.GamePosition.Y] = true;
                }
                if (distanceArray[tile.GamePosition.X, tile.GamePosition.Y] < maxDistance)
                {
                    upNeighbor = squareMap.GetNeighbor(tile, Direction.Up);
                    if (upNeighbor != null)
                    {
                        distanceArray[upNeighbor.GamePosition.X, upNeighbor.GamePosition.Y] = distanceArray[tile.GamePosition.X, tile.GamePosition.Y] + 1;
                        searchQueue.Enqueue(upNeighbor);
                    }

                    downNeighbor = squareMap.GetNeighbor(tile, Direction.Down);
                    if (downNeighbor != null)
                    {
                        distanceArray[downNeighbor.GamePosition.X, downNeighbor.GamePosition.Y] = distanceArray[tile.GamePosition.X, tile.GamePosition.Y] + 1;
                        searchQueue.Enqueue(downNeighbor);
                    }

                    leftNeighbor = squareMap.GetNeighbor(tile, Direction.Left);
                    if (leftNeighbor != null)
                    {
                        distanceArray[leftNeighbor.GamePosition.X, leftNeighbor.GamePosition.Y] = distanceArray[tile.GamePosition.X, tile.GamePosition.Y] + 1;
                        searchQueue.Enqueue(leftNeighbor);
                    }
                    rightNeighbor = squareMap.GetNeighbor(tile, Direction.Right);
                    if (rightNeighbor != null)
                    {
                        distanceArray[rightNeighbor.GamePosition.X, rightNeighbor.GamePosition.Y] = distanceArray[tile.GamePosition.X, tile.GamePosition.Y] + 1;
                        searchQueue.Enqueue(rightNeighbor);
                    }
                }
            }
        }
    }
}