public class BeginnerCorporation : CorporationBase
{
    public BeginnerCorporation()
    {
        Name = "Beginner Corporation";
        Description = "Simpler actions touching only core mechanics, good for learning the core aspects of the game";

        Actions = new GameActionBase[6];
        Actions[0] = new AddExpansionPoints(2);
        Actions[1] = new AddExpansionPoints(2);
        Actions[2] = new AddMarketPoints(2);
        Actions[3] = new AddMarketPoints(2);
        Actions[4] = new AddMarketPoints(2);
        Actions[5] = new MoveDiscAction();
    }
}