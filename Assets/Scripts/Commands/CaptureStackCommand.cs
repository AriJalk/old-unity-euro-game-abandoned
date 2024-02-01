using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.MapSystem;
using EDBG.States;

public class CaptureStackCommand : CommandBase
{
    private ObjectsRenderer renderer;
    private MapTile targetTile;
    private MapTile capturingTile;

    public CaptureStackCommand(LogicState state, MapTile targetTile, MapTile capturingTile, ObjectsRenderer renderer) : base(state)
    {
        this.renderer = renderer;
        this.targetTile = targetTile;
        this.capturingTile = capturingTile;
    }
    public override void ExecuteCommand()
    {
        
    }

    public override void UndoCommand()
    {
        base.UndoCommand();

    }
}