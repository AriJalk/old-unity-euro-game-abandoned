using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using EDBG.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.GameLogic.Core
{
    public static class GameBuilder
    {
        private static MapGrid BuildMap(int mapHeight, int mapWidth)
        {
            Stack<int> faces = new Stack<int>(UtilityFunctions.GetShuffledList(mapHeight, 1, 2, 3, 4, 5, 6));
            Stack<TileColors> colors = new Stack<TileColors>(UtilityFunctions.GetShuffledList(mapHeight, TileColors.Red, TileColors.Green, TileColors.White, TileColors.Black));

            MapGrid mapGrid = new MapGrid(mapHeight, mapWidth);
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    mapGrid.SetCell(new MapTile(new Utilities.DataTypes.GamePosition(i, j), faces.Pop(), colors.Pop()));
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