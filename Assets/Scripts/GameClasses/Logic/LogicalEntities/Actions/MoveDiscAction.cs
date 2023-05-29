using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.Rules;
using System;

public class MoveDiscAction : IAction
{
    public event EventHandler<ActionCompletedEventArgs> ActionCompleted;

    private SquareTile sourceTile;
    private SquareTile targetTile;
    
    private enum Phases
    {
        ChooseSource,
        ChooseTarget,
        Confirm
    }

    private Phases actionPhase;

    public void ExecuteAction(Dictionary<Names.EntityNames, object> logicGameState)
    {
        sourceTile = (SquareTile)logicGameState[Names.EntityNames.SourceTile];
        targetTile = (SquareTile)logicGameState[Names.EntityNames.TargetTile];

        // Check if both source and destination tiles are valid
        if (sourceTile == null || targetTile == null)
        {
            Debug.Log("Invalid source or destination tile");
            return;
        }

        // Check if the source tile has at least one disc
        if (sourceTile.discStack.Count == 0 || sourceTile.discStack.Count == 0)
        {
            Debug.Log("Source tile is empty");
            return;
        }

        // Move the disc from the source stack to the destination stack
        Disc disc = sourceTile.discStack.PopDisc();
        targetTile.discStack.PushDisc(disc);
        OnActionCompleted();
    }

    private void ConfirmAction()
    {
        // Check if both source and destination tiles are valid
        if (sourceTile == null || targetTile == null)
        {
            Debug.Log("Invalid source or destination tile");
            return;
        }

        // Check if the source tile has at least one disc
        if (sourceTile.discStack.Count == 0 || sourceTile.discStack.Count == 0)
        {
            Debug.Log("Source tile is empty");
            return;
        }

        // Move the disc from the source stack to the destination stack
        Disc disc = sourceTile.discStack.PopDisc();
        targetTile.discStack.PushDisc(disc);
        OnActionCompleted();
    }

    protected virtual void OnActionCompleted()
    {
        List<Names.EntityNames> toChange = new List<Names.EntityNames>
        {
            Names.EntityNames.SourceTile,
            Names.EntityNames.TargetTile
        };
        ActionCompleted?.Invoke(this, new ActionCompletedEventArgs(toChange));
    }

    private void SetPhase(Phases actionPhase)
    {
        this.actionPhase = actionPhase;
    }
}
