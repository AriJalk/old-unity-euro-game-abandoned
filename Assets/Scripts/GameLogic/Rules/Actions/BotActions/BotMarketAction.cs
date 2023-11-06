namespace EDBG.GameLogic.Rules
{
    public class BotMarketAction : GameAction
    {
        const string ActionDescription = "Bot performs market action based on existing disc stacks";

        public BotMarketAction(int dieFace) : base($"Market", ActionDescription, dieFace) { }

        public override void ExecuteAction()
        {

        }
    }
}
