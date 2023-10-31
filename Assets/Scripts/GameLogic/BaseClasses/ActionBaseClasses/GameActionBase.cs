public abstract class GameActionBase
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public bool CanExecute {  get; protected set; }

    protected GameActionBase(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public abstract void ExecuteAction();
}
