using System;
using System.Collections;
using System.Collections.Generic;
using EDBG.Rules;

public abstract class GameActionBase
{
    event EventHandler<ActionCompletedEventArgs> ActionCompleted;

    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanExecute {  get; set; }

    public abstract void UpdateCanExecute(LogicGameState gameState);

    public abstract void ExecuteAction(LogicGameState gameState);
}
