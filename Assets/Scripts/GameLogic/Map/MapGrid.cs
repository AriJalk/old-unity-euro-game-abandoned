using System;
using UnityEngine;

namespace EDBG.MapSystem
{
    public class MapGrid : GridContainer
    {
        public MapGrid(int columns, int rows) : base(columns, rows)
        {

        }

        public MapGrid(MapGrid other) : base(other) { }

        public override object Clone()
        {
            return new MapGrid(this);
        }
    }
}


