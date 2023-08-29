using System;
using System.Collections;
using System.Collections.Generic;
using EDBG.Rules;
using UnityEngine;

public interface IAction
{
    event EventHandler<ActionCompletedEventArgs> ActionCompleted;
    void ExecuteAction(LogicGameState gameState);
}
