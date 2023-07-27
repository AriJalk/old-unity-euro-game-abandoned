namespace EDBG.MapSystem
{
    public class MapLocation: ICell
    {
        public GamePosition GamePosition { get; set; }

        private GameStack<Disc> _discStack;

        public GameStack<Disc> DiscStack
        {
            get { return _discStack; }
            set { _discStack = value; }
        }


        public MapLocation(GamePosition position)
        {
            DiscStack= new GameStack<Disc>();
            GamePosition = position;
        }
    }
}
