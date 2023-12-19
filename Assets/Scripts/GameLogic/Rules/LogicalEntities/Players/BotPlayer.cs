using System;

namespace EDBG.GameLogic.Rules
{
    public class BotPlayer : Player
    {
        public BotPlayer(string name, PlayerColors color, int discStock, Corporation corporation) : base(name, color, discStock, corporation)
        {
        }
    }
}
