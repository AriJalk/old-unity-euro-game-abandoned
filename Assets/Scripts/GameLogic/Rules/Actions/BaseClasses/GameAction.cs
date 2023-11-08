namespace EDBG.GameLogic.Rules
{
    public abstract class GameAction
    {
        public Player TargetPlayer { get; set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool CanExecute { get; protected set; }
        public int DieFace { get; protected set; }

        protected GameAction(string name, string description, int dieFace, Player targetPlayer)
        {
            Name = name;
            Description = description;
            CanExecute = false;
            DieFace = dieFace;
            TargetPlayer = targetPlayer;
        }

        public abstract void ExecuteAction();
    }
}

