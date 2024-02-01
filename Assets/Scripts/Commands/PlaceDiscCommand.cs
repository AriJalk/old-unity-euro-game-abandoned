using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Actions;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;

public class PlaceDiscCommand : CommandBase
{
    private readonly PlaceDiscLogic logicCommand;
    private readonly ObjectsRenderer renderer;

    public PlaceDiscCommand(Player activePlayer, MapTile mapTile, ObjectsRenderer renderer) : base(activePlayer)
    {
        logicCommand = new PlaceDiscLogic(activePlayer, mapTile); 
        this.renderer = renderer;
    }
    public override void ExecuteCommand()
    {
        logicCommand.ExecuteCommand();
        if(logicCommand.Disc != null)
            renderer.PlaceNewDisc(logicCommand.Disc, logicCommand.MapTile, true);
    }

    public override void UndoCommand()
    {
        logicCommand.UndoCommand();
        renderer.RemoveTopDisc(logicCommand.MapTile);
    }
}