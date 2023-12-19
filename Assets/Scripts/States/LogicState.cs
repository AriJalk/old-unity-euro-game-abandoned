using System;
using System.Collections.Generic;

using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

namespace EDBG.States
{
    public class LogicState : ICloneable
    {
        public int CurrentPlayerIndex {  get; set; }
        public MapGrid MapGrid { get; set; }

        public List<Player> PlayerList { get; set; }

        public LogicState(MapGrid mapGrid, params Player[] players)
        {
            MapGrid = mapGrid;
            PlayerList = new List<Player>(players);
        }

        private LogicState(LogicState other)
        {
            if (other != null)
            {
                if (other.MapGrid != null)
                {
                    MapGrid = (MapGrid)other.MapGrid.Clone();
                }
                CurrentPlayerIndex = other.CurrentPlayerIndex;
                if (other.PlayerList != null)
                {
                    PlayerList = new List<Player>();
                    foreach(Player player in other.PlayerList)
                    {
                        PlayerList.Add((Player)player.Clone());
                    }
                }
            }
        }

        public object Clone()
        {
            return new LogicState(this);
        }

        public Player GetCurrentPlayer()
        {
            return PlayerList[CurrentPlayerIndex];
        }
    }
}
