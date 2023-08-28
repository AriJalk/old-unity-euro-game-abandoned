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
    private MapLocation sourceLocation;
    private MapLocation targetLocation;

    private enum Phases
    {
        ChooseSource,
        ChooseTarget,
        Confirm
    }

    private Phases actionPhase;
    public void ExecuteAction(Dictionary<EntityNames, object> logicGameState)
    {
        MapLocation sourceLocation = (MapLocation)logicGameState[EntityNames.SourceLocation];
        MapLocation targetLocation = (MapLocation)logicGameState[EntityNames.TargetLocation];

        // Check if both source and destination tiles are valid
        if (sourceLocation == null || targetLocation == null)
        {
            Debug.Log("Invalid source or destination tile");
            OnActionCompleted();
            return;
        }

        // Check if the source tile has at least one disc
        if (((GameStack<Disc>)sourceLocation.LocationComponent.Component).Count == 0)
        {
            Debug.Log("Source tile is empty");
            OnActionCompleted();
            return;
        }

        // Move the disc from the source stack to the destination stack
        Disc disc = ((GameStack<Disc>)sourceLocation.LocationComponent.Component).PopItem();
        ((GameStack<Disc>)targetLocation.LocationComponent.Component).PushItem(disc);
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
        if (((GameStack<Disc>)sourceLocation.LocationComponent.Component).Count == 0 || ((GameStack<Disc>)sourceLocation.LocationComponent.Component).Count == 0)
        {
            Debug.Log("Source tile is empty");
            return;
        }

        // Move the disc from the source stack to the destination stack
        Disc disc = ((GameStack<Disc>)sourceLocation.LocationComponent.Component).PopItem();
        ((GameStack<Disc>)targetLocation.LocationComponent.Component).PushItem(disc);
        OnActionCompleted();
    }

    protected virtual void OnActionCompleted()
    {
        List<EntityNames> toChange = new List<EntityNames>
        {
            EntityNames.SourceLocation,
            EntityNames.TargetLocation,
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
