using EDBG.Rules;
using UnityEngine;

namespace EDBG.MapSystem
{
    public class MapTile : ICell
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

        public int DieFace { get; private set; }

        public GameComponent ComponentOnTile { get; set; }

        public MapTile(GamePosition gamePosition)
        {
            TileType = TileTypes.Default;
            GamePosition = gamePosition;
            DieFace = 1;
        }

        public MapTile(MapTile tile)
        {
            TileType = tile.TileType;
            GamePosition = tile.GamePosition;
        }

        public MapTile(TileTypes type, GamePosition gamePosition)
        {
            TileType = type;
            GamePosition=gamePosition;
        }
    }

}
