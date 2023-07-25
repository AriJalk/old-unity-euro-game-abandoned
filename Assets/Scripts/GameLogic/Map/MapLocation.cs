namespace EDBG.MapSystem
{
    public class MapLocation
    {
        private GameStack<Disc> _discStack;

        public GameStack<Disc> DiscStack
        {
            get { return _discStack; }
            set { _discStack = value; }
        }


        public MapLocation()
        {
            DiscStack= new GameStack<Disc>();
        }
    }
}
