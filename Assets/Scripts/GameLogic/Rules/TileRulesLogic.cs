using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using Unity.VisualScripting;
using System.Linq;

namespace EDBG.Rules
{
    //TODO: Rename class, singleton
    public class TileRulesLogic
    {


        static bool[,] possibleMoves;
        static bool[,] visitedTiles;
        static int[,] distanceArray;
        static int maxDistance;
        static Queue<ICell> searchQueue;
        static GridContainer squareMap;

        public static void Initialize()
        {

        }

        public static bool[,] GetPossibleMoves(GridContainer map, ICell cell, int distance)
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
            maxDistance = distance;
            UpdatePossibleTiles(cell);
            return possibleMoves;
        }

        private static void UpdatePossibleTiles(ICell tile)
        {
            searchQueue = new Queue<ICell>();
            searchQueue.Enqueue(tile);
            distanceArray[tile.GamePosition.Y, tile.GamePosition.X] = 0;
            BFS(tile);
        }

        private static void BFS(ICell tile)
        {
            if (tile == null)
                return;

            while (searchQueue.Count > 0)
            {
                ICell upNeighbor;
                ICell downNeighbor;
                ICell leftNeighbor;
                ICell rightNeighbor;
                tile = searchQueue.Dequeue();
                visitedTiles[tile.GamePosition.Y, tile.GamePosition.X] = true;
                if (distanceArray[tile.GamePosition.Y, tile.GamePosition.X] <= maxDistance)
                {
                    possibleMoves[tile.GamePosition.Y, tile.GamePosition.X] = true;
                }
                if (distanceArray[tile.GamePosition.Y, tile.GamePosition.X] < maxDistance)
                {
                    upNeighbor = squareMap.GetNeighbor(tile, Direction.Up);
                    if (upNeighbor != null)
                    {
                        distanceArray[upNeighbor.GamePosition.Y, upNeighbor.GamePosition.X] = distanceArray[tile.GamePosition.Y, tile.GamePosition.X] + 1;
                        searchQueue.Enqueue(upNeighbor);
                    }

                    downNeighbor = squareMap.GetNeighbor(tile, Direction.Down);
                    if (downNeighbor != null)
                    {
                        distanceArray[downNeighbor.GamePosition.Y, downNeighbor.GamePosition.X] = distanceArray[tile.GamePosition.Y, tile.GamePosition.X] + 1;
                        searchQueue.Enqueue(downNeighbor);
                    }

                    leftNeighbor = squareMap.GetNeighbor(tile, Direction.Left);
                    if (leftNeighbor != null)
                    {
                        distanceArray[leftNeighbor.GamePosition.Y, leftNeighbor.GamePosition.X] = distanceArray[tile.GamePosition.Y, tile.GamePosition.X] + 1;
                        searchQueue.Enqueue(leftNeighbor);
                    }
                    rightNeighbor = squareMap.GetNeighbor(tile, Direction.Right);
                    if (rightNeighbor != null)
                    {
                        distanceArray[rightNeighbor.GamePosition.Y, rightNeighbor.GamePosition.X] = distanceArray[tile.GamePosition.Y, tile.GamePosition.X] + 1;
                        searchQueue.Enqueue(rightNeighbor);
                    }
                }
            }
        }
    }
}