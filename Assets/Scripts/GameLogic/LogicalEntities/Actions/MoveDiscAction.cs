using EDBG.MapSystem;
using EDBG.Rules;
using System;
using System.Collections;
using System.Collections.Generic;

public class MoveDiscAction : GameActionBase
{
    private MapTile sourceTile;
    private MapTile targetTile;
    private int distance;

    public override void ExecuteAction()
    {
        if (CanExecute == true)
        {
            ((GameStack<Disc>)targetTile.ComponentOnTile).PushItem(((GameStack<Disc>)sourceTile.ComponentOnTile).PopItem());
        }
    }


    public MoveDiscAction(MapTile sourceTile, MapTile targetTile, int distance)
    {
        this.sourceTile = sourceTile;
        this.targetTile = targetTile;
        this.distance = distance;
    }

    public override void UpdateCanExecute(LogicGameState gameState)
    {
        if (((GameStack<Disc>)sourceTile.ComponentOnTile).Count == 0)
        {
            CanExecute = false;
            return;
        }
        bool[,] possibleMoves = TileRulesLogic.GetPossibleMoves(gameState.MapGrid, sourceTile, distance);
        if (possibleMoves != null)
        {
            if (possibleMoves[targetTile.GamePosition.Y, targetTile.GamePosition.X] == true)
            {
                CanExecute = true;
                return;
            }
        }
        CanExecute = false;
    }
}
