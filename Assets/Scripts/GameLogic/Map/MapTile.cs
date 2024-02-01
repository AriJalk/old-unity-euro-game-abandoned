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

        //TODO: just disc stack, other components can wait
        public GameStack<Disc> DiscStack { get; set; }

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
            if(tile.DiscStack!= null)
                DiscStack = (GameStack<Disc>)tile.DiscStack.Clone();
        }

        public object Clone()
        {
            return new MapTile(this);
        }

        public Player GetOwner()
        {
            if(DiscStack != null)
            {
                return DiscStack.Owner;
            } 
            return null;
        }

        public override string ToString()
        {
            return $"Tile [{GamePosition.Row},{GamePosition.Col}]";
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(GamePosition, _tileColor, TileColor, DieFace, DiscStack);
        }

        public override bool Equals(object obj)
        {
            return obj is MapTile tile && GamePosition.Equals(tile.GamePosition);
        }
    }

}
