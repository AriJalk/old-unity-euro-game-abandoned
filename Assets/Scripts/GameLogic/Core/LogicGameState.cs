using System;
using System.Collections.Generic;

using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Core
{
    public class LogicGameState : ICloneable
    {

        public MapGrid MapGrid { get; set; }
        public GameStack<Die> RolledDice { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Player> PlayerList { get; set; }

        /*
         * public GameStack<ActionToken> playerTokenBag;
         * public GameStack<ActionToken> playerHand;
         */

        public LogicGameState(MapGrid mapGrid)
        {
            this.MapGrid = mapGrid;
            /*
             * playerTokenBag = new GameStack<ActionToken>();
             * playerHand = new GameStack<ActionToken>();
             */
        }

        private LogicGameState(LogicGameState other)
        {
            if (other != null)
            {
                if (other.MapGrid != null)
                {
                    MapGrid = (MapGrid)other.MapGrid.Clone();
                }
                if (other.RolledDice != null)
                    RolledDice = (GameStack<Die>)other.RolledDice.Clone();  
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
                /*
                 * if (playerTokenBag != null)
                 * playerTokenBag = (GameStack<ActionToken>)other.playerTokenBag.Clone();
                 * if (playerHand != null)
                 * playerHand = (GameStack<ActionToken>)other.playerHand.Clone();
                 */
            }
        }

        public object Clone()
        {
            return new LogicGameState(this);
        }
    }
}
