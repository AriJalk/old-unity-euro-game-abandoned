using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
    public class Disc : IGameComponent
    {
        public DiscColors DiscColor
        {
            get; private set;
        }

        public Disc(DiscColors discColor)
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
