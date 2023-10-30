using System;

public abstract class GameActionBase
{
    event EventHandler<ActionCompletedEventArgs> ActionCompleted;

    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanExecute {  get; set; }

    public abstract void UpdateCanExecute();

    public abstract void ExecuteAction();
}
