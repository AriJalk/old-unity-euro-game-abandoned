namespace EDBG.GameLogic.Rules
{
    public abstract class GameActionAdder : GameAction
    {
        protected Player player;

        private int _bonus;
        public int Bonus
        {
            get
            {
                return _bonus;
            }
            private set
            {
                _bonus = value;
            }
        }

        protected GameActionAdder(string name, string description, int dieFace, int bonus, Player player) : base(name, description, dieFace, player)
        {
            Bonus = bonus;
        }

        public virtual void SetAction(Player player)
        {
            this.player = player;
        }
    }
}
