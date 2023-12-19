using System;

namespace EDBG.GameLogic.Rules
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, PlayerColors color, int discStock, Corporation corporation) : base(name, color, discStock, corporation)
        {
        }
    }
}
