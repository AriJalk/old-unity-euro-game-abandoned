using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    event EventHandler<ActionCompletedEventArgs> ActionCompleted;
    void ExecuteAction(Dictionary<Names.EntityNames, object> logicGameState);
}
