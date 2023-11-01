namespace EDBG.GameLogic.Rules
{
    public abstract class GameActionAdder : GameAction
    {
        public Player Player { get; private set; }
        public readonly int Bonus;

        protected GameActionAdder(string name, string description, int bonus) : base(name, description)
        {
            Bonus = bonus;
        }

        public void SetAction(Player player)
        {
            Player = player;
        }
    }
}
