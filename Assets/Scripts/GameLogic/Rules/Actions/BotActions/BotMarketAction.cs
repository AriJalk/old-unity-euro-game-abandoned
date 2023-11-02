namespace EDBG.GameLogic.Rules
{
    public class BotMarketAction : GameAction
    {
        const string ActionDescription = "Bot performs market action based on existing disc stacks";

        public BotMarketAction() : base($"Market", ActionDescription) { }

        public override void ExecuteAction()
        {

        }
    }
}
