namespace EDBG.GameLogic.Rules
{
    public class BotBuildAction : GameAction
    {
        const string ActionDescription = "Bot adds a disc to an empty or Bot controlled space";

        public BotBuildAction(int dieFace, Player player) : base($"Build", ActionDescription, dieFace, player) { }

        public override void ExecuteAction()
        {
            
        }
    }
}
