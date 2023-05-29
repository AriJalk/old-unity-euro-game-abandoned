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
        static Queue<SquareTile> searchQueue;
        static SquareMap squareMap;

        public static void Initialize(int rows, int columns)
        {
            gridRows = rows;
            gridColumns = columns;
        }

        public static bool[,] GetPossibleMoves(SquareMap map, SquareTile tile, int distance)
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
            UpdatePossibleTiles(squareMap.GetTile(tile.Row, tile.Column));
            return possibleMoves;
        }

        static void UpdatePossibleTiles(SquareTile tile)
        {
            searchQueue = new Queue<SquareTile>();
            searchQueue.Enqueue(tile);
            distanceArray[tile.Row, tile.Column] = 0;
            BFS(tile);
        }

        static void BFS(SquareTile tile)
        {
            if (tile == null)
                return;

            while (searchQueue.Count > 0)
            {
                SquareTile upNeighbor;
                SquareTile downNeighbor;
                SquareTile leftNeighbor;
                SquareTile rightNeighbor;
                tile = searchQueue.Dequeue();
                visitedTiles[tile.Row, tile.Column] = true;
                if (distanceArray[tile.Row, tile.Column] <= maxDistance)
                {
                    possibleMoves[tile.Row, tile.Column] = true;
                }
                if (distanceArray[tile.Row, tile.Column] < maxDistance)
                {
                    upNeighbor = squareMap.GetNeighbor(tile, SquareMap.Direction.Up);
                    if (upNeighbor != null)
                    {
                        distanceArray[upNeighbor.Row, upNeighbor.Column] = distanceArray[tile.Row, tile.Column] + 1;
                        searchQueue.Enqueue(upNeighbor);
                    }

                    downNeighbor = squareMap.GetNeighbor(tile, SquareMap.Direction.Down);
                    if (downNeighbor != null)
                    {
                        distanceArray[downNeighbor.Row, downNeighbor.Column] = distanceArray[tile.Row, tile.Column] + 1;
                        searchQueue.Enqueue(downNeighbor);
                    }

                    leftNeighbor = squareMap.GetNeighbor(tile, SquareMap.Direction.Left);
                    if (leftNeighbor != null)
                    {
                        distanceArray[leftNeighbor.Row, leftNeighbor.Column] = distanceArray[tile.Row, tile.Column] + 1;
                        searchQueue.Enqueue(leftNeighbor);
                    }
                    rightNeighbor = squareMap.GetNeighbor(tile, SquareMap.Direction.Right);
                    if (rightNeighbor != null)
                    {
                        distanceArray[rightNeighbor.Row, rightNeighbor.Column] = distanceArray[tile.Row, tile.Column] + 1;
                        searchQueue.Enqueue(rightNeighbor);
                    }
                }
            }
        }
    }
}