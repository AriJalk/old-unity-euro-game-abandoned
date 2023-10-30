public class BeginnerCorporation : CorporationBase
{
    public BeginnerCorporation()
    {
        Name = "Beginner Corporation";
        Description = "Simpler actions touching only core mechanics, good for learning the core aspects of the game";
        Actions = new GameActionBase[6];
    }
}