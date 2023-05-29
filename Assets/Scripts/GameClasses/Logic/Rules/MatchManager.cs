using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;

namespace EDBG.Rules
{
    public class MatchManager
    {
        private SquareMap matchMap;
        private Player[] matchPlayers;

        public MatchManager(SquareMap squareMap, params Player[] players)
        {
            matchPlayers = new Player[players.Length];
            for (int i = 0; i < players.Length; i++)
                matchPlayers[i] = players[i];
            matchMap=new SquareMap(squareMap);
        }
    }
}
