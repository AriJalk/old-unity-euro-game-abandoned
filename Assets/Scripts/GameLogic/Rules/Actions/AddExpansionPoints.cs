public class AddExpansionPoints : GameActionAdderBase
{
    const string ActionDescription = "Add EP to be used in the current turn";

    public AddExpansionPoints(int bonus) : base($"+{bonus} EP", ActionDescription, bonus) { }

    public override void ExecuteAction()
    {
        Player.ExpansionPoints += Bonus;
    }
}