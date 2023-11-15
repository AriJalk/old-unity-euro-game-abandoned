using EDBG.GameLogic.Rules;
using EDBG.Utilities.DataTypes;
using EDBG.GameLogic.Components;

namespace EDBG.GameLogic.MapSystem
{
    public class MapTile : ICell
    {
        public GamePosition GamePosition { get; set; }

        private TileColors _tileColor;

        public TileColors TileColor
        {
            get
            {
                return _tileColor;
            }
            set
            {
                _tileColor = value;
            }
        }

        public int DieFace { get; private set; }

        public IGameComponent ComponentOnTile { get; set; }

        /// <summary>
        /// Regular constructor
        /// </summary>
        /// <param name="gamePosition"></param>
        public MapTile(GamePosition gamePosition, int dieFace, TileColors color)
        {
            TileColor = color;
            GamePosition = gamePosition;
            DieFace = dieFace;
        }

        public MapTile(MapTile tile)
        {
            TileColor = tile.TileColor;
            GamePosition = tile.GamePosition;
            DieFace = tile.DieFace;
            ComponentOnTile = (IGameComponent)tile.ComponentOnTile.Clone();
        }

        public object Clone()
        {
            return new MapTile(this);
        }
    }

}
