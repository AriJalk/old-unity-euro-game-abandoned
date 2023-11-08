namespace EDBG.GameLogic.Rules
{
    public class BeginnerCorporation : Corporation
    {
        public BeginnerCorporation(Player player) : base(player)
        {
            Name = "Beginner Corporation";
            Description = "Simpler actions touching only core mechanics, good for learning the core aspects of the game";

            switch (player.GetType().Name)
            {
                case "HumanPlayer":
                    BuildHumanCorporation();
                    break;
                case "BotPlayer":
                    BuildBotCorporation();
                    break;
                default:
                    break;
            }

        }

        private void BuildHumanCorporation()
        {
            Actions = new GameAction[6];
            Actions[0] = new AddExpansionPoints(1, 2, player);
            Actions[1] = new AddExpansionPoints(2, 2, player);
            Actions[2] = new AddMarketPoints(3, 2, player);
            Actions[3] = new AddMarketPoints(4, 2, player);
            Actions[4] = new AddMarketPoints(5, 2, player);
            Actions[5] = new MoveDiscAction(6, player);
        }

        private void BuildBotCorporation()
        {
            Actions = new GameAction[6];
            Actions[0] = new BotBuildAction(1, player);
            Actions[1] = new BotBuildAction(2, player);
            Actions[2] = new BotMarketAction(3, player);
            Actions[3] = new BotMarketAction(4, player);
            Actions[4] = new ResearchAction(5, 2, player);
            Actions[5] = new ResearchAction(6, 2, player);
        }
    }
}
