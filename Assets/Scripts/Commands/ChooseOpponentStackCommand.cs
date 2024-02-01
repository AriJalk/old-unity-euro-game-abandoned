using EDBG.Commands;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Actions;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class ChooseOpponentStackCommand : CommandBase
{
    private readonly ObjectsRenderer renderer;

    public List<MapTile> LegalCaptureStackTiles;
    public MapTile OpponentStack { get; private set; }


    public ChooseOpponentStackCommand(LogicState state, MapTile mapTile, ObjectsRenderer renderer) : base(state)
    {
        OpponentStack = mapTile;
        this.renderer = renderer;
        LegalCaptureStackTiles = TileRulesLogic.GetBiggerOpponentStackTiles(mapTile, state);
        if (LegalCaptureStackTiles.Count > 0)
            Result = true;
    }


    public override void ExecuteCommand()
    {
        if(Result == true)
        {
            foreach(MapTile mapTile in LegalCaptureStackTiles)
            {
                renderer.AddAnimationToStack<BreathingAnimation>(mapTile);
            }
            
        }
    }

    public override void UndoCommand()
    {
        base.UndoCommand();
        foreach(MapTile mapTile in LegalCaptureStackTiles)
        {
            renderer.RemoveAnimationFromStack<BreathingAnimation>(mapTile);
        }
    }
}