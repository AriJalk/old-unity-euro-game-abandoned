namespace EDBG.GameLogic.Rules
{
    public class BeginnerCorporation : Corporation
    {
        public BeginnerCorporation(Ownership ownership) : base()
        {
            Name = "Beginner Corporation";
            Description = "Simpler actions touching only core mechanics, good for learning the core aspects of the game";

            switch (ownership)
            {
                case Ownership.HumanPlayer:
                    BuildHumanCorporation();
                    break;
                case Ownership.BotPlayer:
                    BuildBotCorporation();
                    break;
                default:
                    break;
            }

        }

        private void BuildHumanCorporation()
        {
            CorpActions = new CorpAction[6];
            CorpActions[0] = new AddExpansionPointsAction(1, 2);
            CorpActions[1] = new AddExpansionPointsAction(2, 2);
            CorpActions[2] = new AddExpansionPointsAction(3, 2);
            CorpActions[3] = new AddMarketPointsAction(4, 2);
            CorpActions[4] = new AddMarketPointsAction(5, 2);
            CorpActions[5] = new AddMarketPointsAction(6, 2);
        }

        private void BuildBotCorporation()
        {
            CorpActions = new CorpAction[6];
            CorpActions[0] = new AddExpansionPointsAction(1, 2);
            CorpActions[1] = new AddExpansionPointsAction(2, 2);
            CorpActions[2] = new AddExpansionPointsAction(3, 2);
            CorpActions[3] = new AddMarketPointsAction(4, 2);
            CorpActions[4] = new AddMarketPointsAction(5, 2);
            CorpActions[5] = new AddMarketPointsAction(6, 2);
        }
    }
}
