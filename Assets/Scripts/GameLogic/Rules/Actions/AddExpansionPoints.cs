public class AddExpansionPoints : GameActionAdderBase
{
    const string ActionName = "Add Expansion Points (EP)";
    const string ActionDescription = "Add EP to be used in the current turn";

    public AddExpansionPoints(PlayerBase player, int bonus) : base(ActionName, ActionDescription, player, bonus) { }

    public override void ExecuteAction()
    {
        Player.ExpansionPoints += Bonus;
    }
}