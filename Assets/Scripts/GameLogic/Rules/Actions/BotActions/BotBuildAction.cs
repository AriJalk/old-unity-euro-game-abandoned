namespace EDBG.GameLogic.Rules
{
    public class BotBuildAction : GameAction
    {
        const string ActionDescription = "Bot adds a disc to an empty or Bot controlled space";

        public BotBuildAction() : base($"Build", ActionDescription) { }

        public override void ExecuteAction()
        {
            
        }
    }
}
