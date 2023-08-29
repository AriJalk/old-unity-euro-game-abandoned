using EDBG.MapSystem;

namespace EDBG.Rules
{
    public class LogicGameState
    {
        public MapGrid mapGrid;
        public GameStack<Die> rolledDice;

        public MapTile SourceTile = null;
        public MapTile TargetTile = null;


        public LogicGameState(MapGrid mapGrid)
        {
            this.mapGrid = mapGrid;
        }

        
    }
}
