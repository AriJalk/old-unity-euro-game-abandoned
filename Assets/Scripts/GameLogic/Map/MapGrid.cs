using System;
using UnityEngine;

namespace EDBG.MapSystem
{
    public class MapGrid : GridContainer
    {
        public MapGrid() : base(3, 3)
        {
            TestBuild();
        }

        public MapGrid(MapGrid other) : base(other) { }

        private void TestBuild()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    MapTile tile = new MapTile(new GamePosition(i, j));
                    //Set quadrants
                    for (int k = 0; k < tile.Rows; k++)
                    {
                        for (int l = 0; l < tile.Columns; l++)
                        {
                            //Replace with unity engine random
                            System.Random rnd = new System.Random();
                            switch (rnd.Next(0, 3))
                            {
                                case 0:

                                case 1:

                                default:
                                    break;
                            }
                            MapLocation location = (MapLocation)tile.GetCell(k, l);
                            location.DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                            location.DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                            location.DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                            location.DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                        }
                    }

                    SetCell(i, j, tile);
                }
            }
        }
        public MapTile GetTileByLocation(GamePosition gamePosition)
        {
            int globalX = gamePosition.X / Columns;
            int globalY = gamePosition.Y / Rows;
            MapTile tile = (MapTile)GetCellAsContainer(globalX, globalY);
            if (tile == null) return null;
            return tile;
        }

        /// <summary>
        /// Converts global position to internal location in a tile
        /// </summary>
        /// <param name="gamePosition"></param>
        /// <returns></returns>
        public MapLocation GetLocationByGlobal(GamePosition gamePosition)
        {
            MapTile tile = GetTileByLocation(gamePosition);
            if (tile == null) return null;
            int internalX = gamePosition.X % Columns;
            int internalY = gamePosition.Y % Rows;
            MapLocation location=(MapLocation)tile.GetCell(internalX, internalY);
            if(location == null) return null;
            return location;
        } 
    }
}


