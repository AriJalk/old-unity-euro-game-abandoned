using EDBG.GameLogic.Core;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;

namespace EDBG.GameLogic.Rules
{
    public class MoveDiscAction : GameAction
    {
        const string ActionName = "Relocate";
        const string ActionDescription = "Move Disc from a player's controlled stack to a valid position";

        private MapTile sourceTile;
        private MapTile targetTile;

        public MoveDiscAction() : base(ActionName, ActionDescription)
        {

        }

        public override void ExecuteAction()
        {
            if (CanExecute == true)
            {
                ((GameStack<Disc>)targetTile.ComponentOnTile).PushItem(((GameStack<Disc>)sourceTile.ComponentOnTile).PopItem());
                CanExecute = false;
            }
        }

        public void SetAction(MapTile sourceTile, MapTile targetTile, int distance, GameLogicState gameState)
        {
            this.sourceTile = sourceTile;
            this.targetTile = targetTile;
            if (((GameStack<Disc>)sourceTile.ComponentOnTile).Count > 0)
            {
                bool[,] possibleMoves = TileRulesLogic.GetPossibleMoves(gameState.MapGrid, sourceTile, distance);
                CanExecute = (possibleMoves != null && possibleMoves[targetTile.GamePosition.Y, targetTile.GamePosition.X]);
            }
        }


    }
}
