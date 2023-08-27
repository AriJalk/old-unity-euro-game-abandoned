using EDBG.Rules;
using UnityEngine;

namespace EDBG.MapSystem
{
    public class MapTile : GridContainer, ICell
    {
        public GamePosition GamePosition { get; set; }

        private TileTypes _tileType;

        public TileTypes TileType
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


        public MapTile(GamePosition gamePosition) : base(3, 3)
        {
            TileType = TileTypes.Default;
            GamePosition = gamePosition;
            for(int i=0; i < 3; i++)
            {
                for(int j=0; j < 3; j++)
                {
                    SetCell(i, j, new MapLocation(new GamePosition(i, j)));
                }
            }
        }

        public MapTile(MapTile tile) : base(tile)
        {
            TileType = tile.TileType;
            GamePosition = tile.GamePosition;
        }

        public MapTile(TileTypes type, GamePosition gamePosition) : base(3, 3)
        {
            TileType = type;
            GamePosition=gamePosition;
        }
    }

}
