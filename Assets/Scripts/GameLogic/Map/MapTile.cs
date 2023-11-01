using EDBG.GameLogic.Rules;
using EDBG.Utilities.DataTypes;
using EDBG.GameLogic.Components;

namespace EDBG.GameLogic.MapSystem
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

        public IGameComponent ComponentOnTile { get; set; }

        /// <summary>
        /// Regular constructor
        /// </summary>
        /// <param name="gamePosition"></param>
        public MapTile(GamePosition gamePosition, int dieFace)
        {
            TileType = TileTypes.Default;
            GamePosition = gamePosition;
            DieFace = dieFace;
        }

        public MapTile(MapTile tile)
        {
            TileType = tile.TileType;
            GamePosition = tile.GamePosition;
            DieFace = tile.DieFace;
            ComponentOnTile = (IGameComponent)tile.ComponentOnTile.Clone();
        }

        public MapTile(TileTypes type, GamePosition gamePosition)
        {
            TileType = type;
            GamePosition=gamePosition;
        }

        public object Clone()
        {
            return new MapTile(this);
        }
    }

}
