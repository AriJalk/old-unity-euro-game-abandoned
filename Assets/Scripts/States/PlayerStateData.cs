using EDBG.GameLogic.Rules;
using System;

namespace EDBG.States
{
    public class PlayerStateData : ICloneable
    {
        public readonly Player Player;

        public int DiscStock;


        private PlayerStateData(PlayerStateData other)
        {
            Player = other.Player;
            DiscStock = other.DiscStock;
        }

        public PlayerStateData(Player player)
        {
            DiscStock = player.InitialDiscStock;
        }
        public object Clone()
        {
            return new PlayerStateData(this);
        }
    }
}