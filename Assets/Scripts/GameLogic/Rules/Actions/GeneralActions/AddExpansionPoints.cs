namespace EDBG.GameLogic.Rules
{
    public class AddExpansionPoints : GameActionAdder
    {
        const string ActionDescription = "Add EP to be used in the current turn";

        public AddExpansionPoints(int bonus) : base($"+{bonus} EP", ActionDescription, bonus) { }

        public override void ExecuteAction()
        {
            player.ExpansionPoints += Bonus;
        }

        public override void SetAction(Player player)
        {
            base.SetAction(player);
            CanExecute = true;
        }
    }
}
