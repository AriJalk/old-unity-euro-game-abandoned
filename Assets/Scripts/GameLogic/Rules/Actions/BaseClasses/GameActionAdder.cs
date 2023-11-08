namespace EDBG.GameLogic.Rules
{
    public abstract class GameActionAdder : GameAction
    {

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

        protected GameActionAdder(string name, string description, int dieFace, int bonus, Player targetPlayer) : base(name, description, dieFace, targetPlayer)
        {
            Bonus = bonus;
        }

        public abstract void SetAction();
    }
}
