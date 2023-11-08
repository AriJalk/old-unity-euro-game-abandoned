namespace EDBG.GameLogic.Rules
{
    public class AddExpansionPoints : GameActionAdder
    {
        const string ActionDescription = "Add EP to be used in the current turn";

        public AddExpansionPoints(int dieFace, int bonus, Player player) : base($"+{bonus} EP", ActionDescription, dieFace, bonus, player) { }

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
