namespace EDBG.GameLogic.Rules
{
    public class AddMarketPoints : GameActionAdder
    {
        const string ActionDescription = "Add MP to be used in the current turn";

        public AddMarketPoints(int dieFace, int bonus, Player player) : base($"+{bonus} MP", ActionDescription, dieFace, bonus, player) { }

        public override void ExecuteAction()
        {
            TargetPlayer.MarketPoints += Bonus;
        }

        public override void SetAction()
        {
            CanExecute = true;
        }
    }
}
