using EDBG.MapSystem;
using System;
using Unity.VisualScripting;

namespace EDBG.Rules
{
    public class LogicGameState : ICloneable
    {
        public MapGrid mapGrid;
        public GameStack<Die> rolledDice;

        private MapTile _sourceTile;
        public MapTile SourceTile
        {
            get
            {
                return _sourceTile;
            }

            set
            {
                _sourceTile = value;
                mapGrid.SetCell(_sourceTile);
            }
        }

        private MapTile _targetTile;
        public MapTile TargetTile
        {
            get
            {
                return _targetTile;
            }

            set
            {
                _targetTile = value;
                mapGrid.SetCell(_targetTile);
            }
        }


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
            if (other.SourceTile != null)
                SourceTile = (MapTile)other.SourceTile.Clone();
            if(other.TargetTile != null)
                TargetTile = (MapTile)other.TargetTile.Clone();
        }

        public object Clone()
        {
            return new LogicGameState(this);
        }
    }
}
