using EDBG.Rules;

namespace EDBG.MapSystem
{
    public class MapTile
    {
        public GamePosition GamePosition { get; set; }

        private int _rows;

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        private int _columns;

        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        private TileType _tileType;

        public TileType TileType
        {
            get
            {
                return _tileType;
            }
            set
            {
                _tileType = value;
            }
        }

        private MapLocation[,] _innerGrid;

        public MapLocation[,] InnerGrid
        {
            get { return _innerGrid; }
            set { _innerGrid = value; }
        }



        public MapTile(GamePosition gamePosition)
        {
            TileType = TileType.Default;
            GamePosition = gamePosition;
            InnerGrid= new MapLocation[3, 3];
            for(int i=0; i < 3; i++)
            {
                for(int j=0; j < 3; j++)
                {
                    InnerGrid[i, j]=new MapLocation();
                }
            }
            Rows = 3;
            Columns = 3;
        }

        public MapTile(MapTile tile)
        {
            TileType = tile.TileType;
            GamePosition = tile.GamePosition;
            InnerGrid= tile.InnerGrid;
        }

        public MapTile(TileType type, GamePosition gamePosition)
        {
            TileType = type;
            GamePosition=gamePosition;
            InnerGrid= new MapLocation[3, 3];
        }
    }

}
