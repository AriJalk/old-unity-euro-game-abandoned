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
        }

        private Disc(Disc other)
        {
            _owner = other._owner;
        }

        public override object Clone()
        {
            return new Disc(this);
        }
    }
}
