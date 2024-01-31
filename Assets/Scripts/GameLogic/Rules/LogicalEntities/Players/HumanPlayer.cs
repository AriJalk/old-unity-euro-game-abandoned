using System;

namespace EDBG.GameLogic.Rules
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, PlayerColors playerColor, PlayerColors fillerColor, int discStock, Corporation corporation) :
            base(name, playerColor, fillerColor, discStock, corporation)
        {
        }
    }
}
