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

        public List<PlayerStateData> PlayerStateList { get; set; }

        public LogicState(MapGrid mapGrid, params Player[] players)
        {
            MapGrid = mapGrid;
            PlayerStateList = new List<PlayerStateData>();
            foreach(Player player in players)
            {
                PlayerStateList.Add(new PlayerStateData(player));
            }
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
                if (other.PlayerStateList != null)
                {
                    PlayerStateList = new List<PlayerStateData>();
                    foreach(PlayerStateData stateData in other.PlayerStateList)
                    {
                        PlayerStateList.Add((PlayerStateData)stateData.Clone());
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
            return PlayerStateList[CurrentPlayerIndex].Player;
        }
    }
}
