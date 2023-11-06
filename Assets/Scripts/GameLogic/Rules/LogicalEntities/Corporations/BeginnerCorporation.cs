namespace EDBG.GameLogic.Rules
{
    public class BeginnerCorporation : Corporation
    {
        public BeginnerCorporation(Ownership ownership) : base(ownership)
        {
            Name = "Beginner Corporation";
            Description = "Simpler actions touching only core mechanics, good for learning the core aspects of the game";

            switch (ownership)
            {
                case Ownership.Player:
                    BuildHumanCorporation();
                    break;
                case Ownership.Bot:
                    BuildBotCorporation();
                    break;
                default:
                    break;
            }

        }

        private void BuildHumanCorporation()
        {
            Actions = new GameAction[6];
            Actions[0] = new AddExpansionPoints(1, 2);
            Actions[1] = new AddExpansionPoints(2, 2);
            Actions[2] = new AddMarketPoints(3, 2);
            Actions[3] = new AddMarketPoints(4, 2);
            Actions[4] = new AddMarketPoints(5, 2);
            Actions[5] = new MoveDiscAction(6);
        }

        private void BuildBotCorporation()
        {
            Actions = new GameAction[6];
            Actions[0] = new BotBuildAction(1);
            Actions[1] = new BotBuildAction(2);
            Actions[2] = new BotMarketAction(3);
            Actions[3] = new BotMarketAction(4);
            Actions[4] = new ResearchAction(5, 2);
            Actions[5] = new ResearchAction(6, 2);
        }
    }
}
