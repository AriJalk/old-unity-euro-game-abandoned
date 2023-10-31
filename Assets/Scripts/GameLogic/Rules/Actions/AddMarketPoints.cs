public class AddMarketPoints : GameActionAdderBase
{
    const string ActionName = "Add Market Points (MP)";
    const string ActionDescription = "Add MP to be used in the current turn";

    public AddMarketPoints(PlayerBase player, int bonus) : base(ActionName, ActionDescription, player, bonus) { }

    public override void ExecuteAction()
    {
        Player.MarketPoints += Bonus;
    }
}