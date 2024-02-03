using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.States;
using System.Collections.Generic;

public class CaptureStackCommand : CommandBase
{
    private ObjectsRenderer renderer;
    private MapTile targetTile;
    private MapTile capturingTile;

    private GameStack<Disc> undoDiscStack;

    public CaptureStackCommand(LogicState state, MapTile targetTile, MapTile capturingTile, ObjectsRenderer renderer) : base(state)
    {
        this.renderer = renderer;
        this.targetTile = targetTile;
        this.capturingTile = capturingTile;
        undoDiscStack = targetTile.DiscStack.Clone() as GameStack<Disc>;
    }
    public override void ExecuteCommand()
    {


        // Move stack to capture
        GameStack<Disc> tempStack = targetTile.DiscStack;
        // TODO: Possible rule variant in the future where stack is not cleared
        tempStack.ClearStack();
        renderer.RenderObjectsOnTileObject(targetTile, false);
        targetTile.DiscStack = capturingTile.DiscStack;
        // Place empty stack instead of creating a new one
        capturingTile.DiscStack = tempStack;
        Result = true;
        renderer.AddCaptureAnimation(capturingTile, targetTile);

    }

    public override void UndoCommand()
    {
        base.UndoCommand();
        capturingTile.DiscStack = targetTile.DiscStack;
        targetTile.DiscStack = undoDiscStack;
        renderer.RenderObjectsOnTileObject(targetTile, false);
        renderer.RenderObjectsOnTileObject(capturingTile, false);
    }
}