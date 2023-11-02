namespace EDBG.GameLogic.Rules
{
    public abstract class GameActionAdder : GameAction
    {
        protected Player player;

        public readonly int Bonus;

        protected GameActionAdder(string name, string description, int bonus) : base(name, description)
        {
            Bonus = bonus;
        }

        public virtual void SetAction(Player player)
        {
            this.player = player;
        }
    }
}
