namespace EDBG.GameLogic.Rules
{
    public class BotMarketAction : GameAction
    {
        const string ActionDescription = "Bot performs market action based on existing disc stacks";

        public BotMarketAction(int dieFace, Player player) : base($"Market", ActionDescription, dieFace, player) { }

        public override void ExecuteAction()
        {

        }
    }
}
