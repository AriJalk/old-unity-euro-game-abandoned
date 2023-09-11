using EDBG.MapSystem;
using System;
using Unity.VisualScripting;


namespace EDBG.Rules
{
    public class LogicGameState : ICloneable
    {
        public MapGrid mapGrid;
        public GameStack<Die> rolledDice;
        public GameStack<ActionToken> playerTokenBag;
        public GameStack<ActionToken> playerHand;

        public LogicGameState(MapGrid mapGrid)
        {
            this.mapGrid = mapGrid;
            playerTokenBag = new GameStack<ActionToken>();
            playerHand = new GameStack<ActionToken>();
        }

        private LogicGameState(LogicGameState other)
        {
            if (other != null)
            {
                if (other.mapGrid != null)
                {
                    mapGrid = (MapGrid)other.mapGrid.Clone();
                }
                if (other.rolledDice != null)
                    rolledDice = (GameStack<Die>)other.rolledDice.Clone();
                if (playerTokenBag != null)
                    playerTokenBag = (GameStack<ActionToken>)other.playerTokenBag.Clone();
                if (playerHand != null)
                    playerHand = (GameStack<ActionToken>)other.playerHand.Clone();
            }
        }

        public object Clone()
        {
            return new LogicGameState(this);
        }
    }
}
