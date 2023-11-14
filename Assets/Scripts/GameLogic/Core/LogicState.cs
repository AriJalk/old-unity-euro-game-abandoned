using System;
using System.Collections.Generic;

using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Core
{
    public class LogicState : ICloneable
    {
        public int CurrentPlayerIndex {  get; set; }
        public MapGrid MapGrid { get; set; }
        public DiceTray DiceTray { get; set; }



        public List<Player> PlayerList { get; set; }

        public LogicState(MapGrid mapGrid)
        {
            MapGrid = mapGrid;
            PlayerList = new List<Player>();
        }

        private LogicState(LogicState other)
        {
            if (other != null)
            {
                if (other.MapGrid != null)
                {
                    MapGrid = (MapGrid)other.MapGrid.Clone();
                }
                if (other.DiceTray != null)
                    DiceTray = (DiceTray)other.DiceTray.Clone();  
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
