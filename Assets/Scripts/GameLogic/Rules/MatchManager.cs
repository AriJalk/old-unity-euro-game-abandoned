using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;

namespace EDBG.Rules
{
    public class MatchManager
    {
        private LogicGameState gameState;
        private readonly Player[] matchPlayers;

        public MatchManager(MapGrid squareMap, params Player[] players)
        {
            matchPlayers = new Player[players.Length];
            for (int i = 0; i < players.Length; i++)
                matchPlayers[i] = players[i];
            gameState = new LogicGameState(squareMap);
        }
    }
}
