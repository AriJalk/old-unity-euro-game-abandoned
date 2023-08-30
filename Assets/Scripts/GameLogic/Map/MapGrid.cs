using System;
using UnityEngine;

namespace EDBG.MapSystem
{
    public class MapGrid : GridContainer
    {
        public MapGrid() : base(9, 9)
        {
            TestBuild();
        }

        public MapGrid(MapGrid other) : base(other) { }

        public override object Clone()
        {
            return new MapGrid(this);
        }

        private void TestBuild()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    MapTile tile = new MapTile(new GamePosition(i, j));
                    SetCell(tile);
                    tile.ComponentOnTile = new GameStack<Disc>();
                    ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                    ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                    ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                    ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                }
            }
        }
    }
}


