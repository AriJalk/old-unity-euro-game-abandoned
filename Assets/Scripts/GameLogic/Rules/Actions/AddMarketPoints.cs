public class AddMarketPoints : GameActionAdderBase
{
    const string ActionDescription = "Add MP to be used in the current turn";

    public AddMarketPoints(int bonus) : base($"+{bonus} MP", ActionDescription, bonus) { }

    public override void ExecuteAction()
    {
        Player.MarketPoints += Bonus;
    }
}