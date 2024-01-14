using EDBG.GameLogic.Rules;
using EDBG.Utilities.DataTypes;
using EDBG.GameLogic.Components;
using System.Collections.Generic;
using System;

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
            if(tile.ComponentOnTile!= null)
                ComponentOnTile = (IGameComponent)tile.ComponentOnTile.Clone();
        }

        public object Clone()
        {
            return new MapTile(this);
        }

        public Player GetOwner()
        {
            if(ComponentOnTile != null)
            {
                return ComponentOnTile.Owner;
            } 
            return null;
        }


        public T GetComponentOnTile<T>() where T: IGameComponent
        {
            return (T)ComponentOnTile;
        }

        public override string ToString()
        {
            return $"Tile [{GamePosition.Row},{GamePosition.Col}]";
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(GamePosition, _tileColor, TileColor, DieFace, ComponentOnTile);
        }

        public override bool Equals(object obj)
        {
            return obj is MapTile tile &&
                   EqualityComparer<GamePosition>.Default.Equals(GamePosition, tile.GamePosition) &&
                   _tileColor == tile._tileColor &&
                   TileColor == tile.TileColor &&
                   DieFace == tile.DieFace &&
                   EqualityComparer<IGameComponent>.Default.Equals(ComponentOnTile, tile.ComponentOnTile);
        }
    }

}
