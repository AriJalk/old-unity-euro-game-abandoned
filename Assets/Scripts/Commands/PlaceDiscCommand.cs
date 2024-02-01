using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Actions;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;

public class PlaceDiscCommand : CommandBase
{
    private readonly PlaceDiscLogic logicCommand;
    private readonly ObjectsRenderer renderer;

    public PlaceDiscCommand(LogicState state, MapTile mapTile, ObjectsRenderer renderer) : base(state)
    {
        logicCommand = new PlaceDiscLogic(state, mapTile); 
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
        base.UndoCommand();
        logicCommand.UndoCommand();
        renderer.RemoveTopDisc(logicCommand.MapTile);
    }
}