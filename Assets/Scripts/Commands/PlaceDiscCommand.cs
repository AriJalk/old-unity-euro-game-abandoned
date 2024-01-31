using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Actions;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;

public class PlaceDiscCommand : CommandBase
{
    private PlaceDiscLogic logicCommand;
    private ObjectsRenderer renderer;

    public PlaceDiscCommand(MapTile mapTile, Player player, ObjectsRenderer renderer)
    {
        logicCommand = new PlaceDiscLogic(mapTile, player); 
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