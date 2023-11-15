using System.Collections.Generic;

namespace EDBG.GameLogic.Rules
{
    public abstract class CorpAction
    {
        public List<int> DieFaces { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
		public bool CanExecute {  get; protected set; }

        public abstract void ExecuteAction();
        public abstract void SetAction(Player player);

    }
}