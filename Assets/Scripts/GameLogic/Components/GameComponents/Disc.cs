using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
    public class Disc : IGameComponent
    {
        public PlayerColors DiscColor
        {
            get; private set;
        }

        public Disc(PlayerColors discColor)
        {
            DiscColor = discColor;
        }

        protected Disc(Disc other)
        {
            DiscColor = other.DiscColor;
        }

        public override object Clone()
        {
            return new Disc(this);
        }
    }
}
