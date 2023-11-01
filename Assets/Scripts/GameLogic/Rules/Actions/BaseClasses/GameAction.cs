namespace EDBG.GameLogic.Rules
{
    public abstract class GameAction
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool CanExecute { get; protected set; }

        protected GameAction(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void ExecuteAction();
    }
}

