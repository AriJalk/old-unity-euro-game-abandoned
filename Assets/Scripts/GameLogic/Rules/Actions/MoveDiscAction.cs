using EDBG.MapSystem;
using EDBG.Rules;

public class MoveDiscAction : GameActionBase
{
    const string ActionName = "Relocate Disc";
    const string ActionDescription = "Move Disc from a player's controlled stack to a valid position";

    private readonly MapTile sourceTile;
    private readonly MapTile targetTile;
    private readonly int distance;
    private readonly LogicGameState gameState;

    public MoveDiscAction(MapTile sourceTile, MapTile targetTile, int distance, LogicGameState gameState) : base(ActionName, ActionDescription)
    {
        this.sourceTile = sourceTile;
        this.targetTile = targetTile;
        this.distance = distance;
        this.gameState = gameState;
        UpdateCanExecute();
    }

    public override void ExecuteAction()
    {
        if (CanExecute == true)
        {
            ((GameStack<Disc>)targetTile.ComponentOnTile).PushItem(((GameStack<Disc>)sourceTile.ComponentOnTile).PopItem());
        }
    }

    private void UpdateCanExecute()
    {
        if (((GameStack<Disc>)sourceTile.ComponentOnTile).Count > 0)
        {
            bool[,] possibleMoves = TileRulesLogic.GetPossibleMoves(gameState.MapGrid, sourceTile, distance);
            CanExecute = (possibleMoves != null && possibleMoves[targetTile.GamePosition.Y, targetTile.GamePosition.X]);
        }
    }


}