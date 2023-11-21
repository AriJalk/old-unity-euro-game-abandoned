using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using EDBG.Utilities;
using EDBG.Utilities.DataTypes;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.GameLogic.Core
{
    public static class GameBuilder
    {
        private static MapGrid BuildMap(int mapHeight, int mapWidth)
        {
            Stack<int> faces = new Stack<int>(UtilityFunctions.GetShuffledList(mapHeight, 1, 2, 3, 4, 5, 6));
            TileColors[] colorsArray = {TileColors.Green, TileColors.Green, TileColors.Green,TileColors.Green, TileColors.Red, TileColors.Red, TileColors.Red, TileColors.Red,
                TileColors.Blue, TileColors.Blue, TileColors.Blue, TileColors.Blue, TileColors.Black, TileColors.Black,
            };
            List<TileColors> shuffledColorList = new List<TileColors>(colorsArray);
            UtilityFunctions.ShuffleList(shuffledColorList);
            Stack<TileColors> tileColorsStack = new Stack<TileColors>(shuffledColorList);
            MapGrid mapGrid = new MapGrid(mapHeight, mapWidth);
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    MapTile tile;

                    if (i == 0 && j == 0)
                    {
                        tile = new MapTile(new GamePosition(i, j), faces.Pop(), TileColors.White);
                        tile.ComponentOnTile = new GameStack<Disc>();
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.Black));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.Black));
                    }
                    else if (i == 3 && j == 3)
                    {
                        tile = new MapTile(new GamePosition(i, j), faces.Pop(), TileColors.White);
                        tile.ComponentOnTile = new GameStack<Disc>();
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.White));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.White));
                    }
                    else
                    {
                        tile = new MapTile(new GamePosition(i, j), faces.Pop(), tileColorsStack.Pop());
                    }
                    mapGrid.SetCell(tile);
                }
            }

            return mapGrid;
        }

        public static GameState BuildInitialState(int mapWidth, int mapHeight, params Player[] players)
        {
            GameState state;
            MapGrid mapGrid = BuildMap(mapHeight, mapWidth);
            LogicState logicState = new LogicState(mapGrid, players);
            UIState uiState = new UIState();
            state = new GameState(logicState, uiState);

            return state;
        }


    }
}