using EDBG.Rules;

namespace EDBG.MapSystem
{
    public class MapLocation: ICell
    {
        public GamePosition GamePosition { get; set; }
        public LocationTypes LocationType { get; set; }

        public LocationComponent LocationComponent { get; set; }

        //TODO: ownership logic
        public MapLocation(GamePosition position)
        {
            GamePosition = position;
            LocationComponent = new LocationComponent(new GameStack<Disc>(),Ownership.Neutral);
        }
    }
}
