using System;

namespace EDBG.GameLogic.Rules
{
    public class BotPlayer : Player
    {
        public BotPlayer(string name, PlayerColors playerColor, PlayerColors fillerColor, int discStock, Corporation corporation) :
            base(name, playerColor, fillerColor, discStock, corporation)
        {
        }
    }
}
