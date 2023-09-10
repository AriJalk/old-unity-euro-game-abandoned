using EDBG.MapSystem;
using System;
using Unity.VisualScripting;


namespace EDBG.Rules
{
    public class LogicGameState : ICloneable
    {
        public MapGrid mapGrid;
        public GameStack<Die> rolledDice;

        public LogicGameState(MapGrid mapGrid)
        {
            this.mapGrid = mapGrid;
        }

        public LogicGameState(LogicGameState other)
        {
            if(other.mapGrid!=null)
                mapGrid = (MapGrid)other.mapGrid.Clone();
            if(other.rolledDice!=null)
            rolledDice = (GameStack<Die>)other.rolledDice.Clone();
        }

        public object Clone()
        {
            return new LogicGameState(this);
        }
    }
}
