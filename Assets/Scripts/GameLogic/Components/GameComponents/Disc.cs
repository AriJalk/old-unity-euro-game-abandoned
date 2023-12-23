using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
    public class Disc : IGameComponent
    {
        private Player _owner;
        public override Player Owner {
            get
            { 
                return _owner; 
            } 
        }
        public PlayerColors DiscColor
        {
            get; private set;
        }

        public override bool IsNull
        {
            get
            {
                if (Owner == null)
                    return true;
                return false;
            }
        }

        public Disc(Player player)
        {
            _owner = player;
            DiscColor = player.PlayerColor;
        }

        private Disc(Disc other)
        {
            _owner = other._owner;
            DiscColor = other.DiscColor;
        }

        public override object Clone()
        {
            return new Disc(this);
        }
    }
}
