using System.Collections.Generic;

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

        }

        private void BuildBotCorporation()
        {

        }
    }
}
