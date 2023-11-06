namespace EDBG.GameLogic.Rules
{
    public class BotBuildAction : GameAction
    {
        const string ActionDescription = "Bot adds a disc to an empty or Bot controlled space";

        public BotBuildAction(int dieFace) : base($"Build", ActionDescription, dieFace) { }

        public override void ExecuteAction()
        {
            
        }
    }
}
