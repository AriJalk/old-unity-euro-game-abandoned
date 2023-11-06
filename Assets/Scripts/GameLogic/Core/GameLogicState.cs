using System;
using System.Collections.Generic;

using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Core
{
    public class GameLogicState : ICloneable
    {

        public MapGrid MapGrid { get; set; }
        public DiceTray DiceTray { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Player> PlayerList { get; set; }

        public GameLogicState(MapGrid mapGrid)
        {
            this.MapGrid = mapGrid;
        }

        private GameLogicState(GameLogicState other)
        {
            if (other != null)
            {
                if (other.MapGrid != null)
                {
                    MapGrid = (MapGrid)other.MapGrid.Clone();
                }
                if (other.DiceTray != null)
                    DiceTray = (DiceTray)other.DiceTray.Clone();  
                if (other.CurrentPlayer != null)
                    CurrentPlayer = (Player)other.CurrentPlayer.Clone();
                if (other.PlayerList != null)
                {
                    PlayerList = new List<Player>();
                    foreach(Player player in other.PlayerList)
                    {
                        PlayerList.Add(player);
                    }
                }
            }
        }

        public object Clone()
        {
            return new GameLogicState(this);
        }
    }
}
