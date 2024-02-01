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
            List<MapTile> tiles = GetTilesWithComponentInAllDirections(map, originTile, player, true, true);
            int tilesWithPlayerDiscs = tiles.Count;

            // 2nd floor requires 1 additional disc from connected tile, 3 requires 2 etc
            if (tilesWithPlayerDiscs < (originTile.DiscStack).Count + 1)
                return false;
            return true;
        }

        public static List<MapTile> GetTilesThatContribute(GridContainer map, MapTile originTile, Player player)
        {

            List<MapTile> tiles = GetTilesWithComponentInAllDirections(map, originTile, player, true, true);

            // 2nd floor requires 1 additional disc from connected tile, 3 requires 2 etc
            if (tiles.Count > originTile.DiscStack.Count)
                return tiles;


            return null;
        }

        private static List<MapTile> GetTilesWithComponentInDirection(GridContainer map, MapTile originTile, Player player, bool canBlock, bool isPlayerOwned, Direction direction)
        {
            List<MapTile> tiles = new List<MapTile>();
            while (map.GetNeighbor(originTile, direction) != null)
            {
                originTile = map.GetNeighbor(originTile, direction) as MapTile;
                if (originTile.DiscStack.Count > 0)
                {
                    if (originTile.DiscStack.Owner == player)
                    {
                        tiles.Add(originTile);
                    }

                    if (canBlock)
                        break;
                }
            }
            return tiles;
        }

        public static List<MapTile> GetTilesWithComponentInAllDirections(GridContainer map, MapTile originTile, Player player, bool canBlock, bool isPlayerOwned)
        {
            List<MapTile> tiles = GetTilesWithComponentInDirection(map, originTile, player, canBlock, isPlayerOwned, Direction.Left);
            tiles.AddRange(GetTilesWithComponentInDirection(map, originTile, player, canBlock, isPlayerOwned, Direction.Right));
            tiles.AddRange(GetTilesWithComponentInDirection(map, originTile, player, canBlock, isPlayerOwned, Direction.Up));
            tiles.AddRange(GetTilesWithComponentInDirection(map, originTile, player, canBlock, isPlayerOwned, Direction.Down));

            return tiles;
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
        public static bool[,] GetCellsInDistance(GridContainer map, MapTile tile, int distance)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch();
            bool[,] distanceMap = bfs.Search(map, tile, distance);
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

        public static List<MapTile> GetTilesInDirectLine(GridContainer map, MapTile originTile, bool canBlock)
        {
            List<MapTile> tiles = new List<MapTile>();

            // Get cells in column
            for (int i = 0; i < map.Rows; i++)
            {
                if (i != originTile.GamePosition.Row)
                {
                    tiles.Add(map.GetCell<MapTile>(i, originTile.GamePosition.Col));
                }
            }

            // Get cells in row
            for (int i = 0; i < map.Columns; i++)
            {
                if (i != originTile.GamePosition.Col)
                {
                    tiles.Add(map.GetCell<MapTile>(originTile.GamePosition.Row, i));
                }
            }

            return tiles;
        }

        public static bool IsTileValid(ChooseTile chooseTile)
        {
            if (chooseTile.SelectedTile.DiscStack == null)
            {
                return true;
            }
            else if (
                chooseTile.SelectedTile.DiscStack.Owner == chooseTile.LogicState.GetCurrentPlayer() ||
                chooseTile.SelectedTile.DiscStack.Owner == null)
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
            if (chooseTile.SelectedTile.DiscStack == null)
                chooseTile.SelectedTile.DiscStack = new GameStack<Disc>();
            ((GameStack<Disc>)chooseTile.SelectedTile.DiscStack).PushItem(new Disc(chooseTile.LogicState.GetCurrentPlayer()));
        }

        public static Disc AddDiscToTile(MapTile mapTile, Player player)
        {
            Disc disc = null;
            if (mapTile != null && player != null)
            {
                if (player.DiscStock > 0)
                {
                    mapTile.DiscStack ??= new GameStack<Disc>();
                    GameStack<Disc> stack = mapTile.DiscStack;
                    disc = new Disc(player);
                    stack.PushItem(disc);
                    player.DiscStock--;
                }
            }
            return disc;
        }

        public static void RemoveTopDiscFromTile(MapTile mapTile)
        {
            if (mapTile != null)
            {
                mapTile.DiscStack.PopItem();
            }
        }

        public static List<MapTile> GetBiggerOpponentStackTiles(ChooseTile chosenTile)
        {
            GameStack<Disc> originStack = chosenTile.SelectedTile.DiscStack;
            if (originStack == null)
                return null;
            int stackSize = originStack.Count;
            Player player = chosenTile.LogicState.GetCurrentPlayer();

            List<MapTile> tiles = GetTilesWithComponentInAllDirections(chosenTile.LogicState.MapGrid, chosenTile.SelectedTile, player, true, true);
            List<MapTile> largerTiles = new List<MapTile>();
            if (tiles.Count > 0)
            {
                foreach (MapTile tile in tiles)
                {

                    GameStack<Disc> stack = tile.DiscStack as GameStack<Disc>;
                    if (stack.Count > stackSize)
                        largerTiles.Add(tile);
                }
            }
            return largerTiles;
        }
    }
}
