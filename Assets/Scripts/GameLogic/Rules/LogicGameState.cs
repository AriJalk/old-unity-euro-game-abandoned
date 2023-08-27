using EDBG.MapSystem;

namespace EDBG.Rules
{
    public class GameState
    {
        private MapGrid mapGrid;
        private GameStack<Die> rolledDice;



        public GameState(MapGrid mapGrid)
        {
            this.mapGrid = mapGrid;
        }

        
    }
}
