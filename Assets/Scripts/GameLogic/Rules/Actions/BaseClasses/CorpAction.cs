namespace EDBG.GameLogic.Rules
{
    public abstract class CorpAction
    {
        public int DieFace { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
		public bool CanExecute {  get; protected set; }

        public abstract void ExecuteAction();
        public abstract void SetAction(Player player);

    }
}