using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
    public class Disc : IGameComponent
    {
        public Player Player { get; private set; }
        public PlayerColors DiscColor
        {
            get; private set;
        }

        public Disc(Player player)
        {
            DiscColor = player.PlayerColor;
        }

        private Disc(Disc other)
        {
            Player = other.Player;
            DiscColor = other.DiscColor;
        }

        public override object Clone()
        {
            return new Disc(this);
        }
    }
}
