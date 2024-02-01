using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Actions;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;

public class PlaceDiscsCommand : CommandBase
{
    private readonly PlaceDiscsLogic logicCommand;
    private readonly ObjectsRenderer renderer;

    public new bool Result 
    {
        get
        {
            return logicCommand.Result;
        }
    }

    public PlaceDiscsCommand(LogicState state, MapTile mapTile, ObjectsRenderer renderer) : base(state)
    {
        logicCommand = new PlaceDiscsLogic(state, mapTile); 
        this.renderer = renderer;
    }
    public override void ExecuteCommand()
    {
        logicCommand.ExecuteCommand();
        if(Result == true)
        {
            foreach(Disc disc in  logicCommand.DiscList)
            {
                renderer.PlaceNewDisc(disc, logicCommand.MapTile, true);
            }
        }
    }

    public override void UndoCommand()
    {
        base.UndoCommand();
        int discCount = logicCommand.DiscList.Count;
        while(discCount > 0)
        {
            renderer.RemoveTopDisc(logicCommand.MapTile);
            discCount--;
        }
        logicCommand.UndoCommand();
    }
}