namespace EDBG.GameLogic.Rules
{
    public class AddMarketPoints : GameActionAdder
    {
        const string ActionDescription = "Add MP to be used in the current turn";

        public AddMarketPoints(int bonus) : base($"+{bonus} MP", ActionDescription, bonus) { }

        public override void ExecuteAction()
        {
            player.MarketPoints += Bonus;
        }

        public override void SetAction(Player player)
        {
            base.SetAction(player);
            CanExecute = true;
        }
    }
}
