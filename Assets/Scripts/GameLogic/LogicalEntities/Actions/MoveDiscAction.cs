using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.Rules;
using EDBG.MapSystem;
using System;
using System.Reflection;

public class MoveDiscAction : IAction
{
    public event EventHandler<ActionCompletedEventArgs> ActionCompleted;

    private MapTile sourceTile;
    private MapTile targetTile;

    private enum Phases
    {
        ChooseSource,
        ChooseTarget,
        Confirm
    }

    private Phases actionPhase;
    public void ExecuteAction(LogicGameState gameState)
    {
        sourceTile = gameState.SourceTile;
        targetTile = gameState.TargetTile;    // Check if both source and destination tiles are valid
        if (sourceTile == null || targetTile == null)
        {
            Debug.Log("Invalid source or destination tile");
            OnActionCompleted();
            return;
        }

        // Check if the source tile has at least one disc
        if (((GameStack<Disc>)sourceTile.ComponentOnTile).Count == 0)
        {
            Debug.Log("Source tile is empty");
            OnActionCompleted();
            return;
        }

        // Move the disc from the source stack to the destination stack
        Disc disc = ((GameStack<Disc>)sourceTile.ComponentOnTile).PopItem();
        ((GameStack<Disc>)targetTile.ComponentOnTile).PushItem(disc);
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
        if (((GameStack<Disc>)sourceTile.ComponentOnTile).Count == 0 || ((GameStack<Disc>)sourceTile.ComponentOnTile).Count == 0)
        {
            Debug.Log("Source tile is empty");
            return;
        }

        // Move the disc from the source stack to the destination stack
        Disc disc = ((GameStack<Disc>)sourceTile.ComponentOnTile).PopItem();
        ((GameStack<Disc>)targetTile.ComponentOnTile).PushItem(disc);
        OnActionCompleted();
    }

    protected virtual void OnActionCompleted()
    {
        List<EntityNames> toChange = new List<EntityNames>
        {
            EntityNames.SourceTile,
            EntityNames.TargetTile,
        };
        ActionCompleted?.Invoke(this, new ActionCompletedEventArgs(toChange));
    }

    private void SetPhase(Phases actionPhase)
    {
        this.actionPhase = actionPhase;
    }
}
