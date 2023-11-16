using EDBG.Utilities.DataTypes;

namespace EDBG.GameLogic.MapSystem
{
    public class MapGrid : GridContainer
    {
        public MapGrid(int rows, int columns) : base(rows, columns)
        {

        }

        public MapGrid(MapGrid other) : base(other) { }

        public override object Clone()
        {
            return new MapGrid(this);
        }
    }
}


