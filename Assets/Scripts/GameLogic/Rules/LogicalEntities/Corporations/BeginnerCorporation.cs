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
            Actions[0] = new AddExpansionPoints(2);
            Actions[1] = new AddExpansionPoints(2);
            Actions[2] = new AddMarketPoints(2);
            Actions[3] = new AddMarketPoints(2);
            Actions[4] = new AddMarketPoints(2);
            Actions[5] = new MoveDiscAction();
        }

        private void BuildBotCorporation()
        {
            Actions = new GameAction[3];
            //TODO: define actions
            Actions[0] = new BotBuildAction();
            Actions[1] = new BotMarketAction();
            Actions[2] = new ResearchAction(1);
        }
    }
}
