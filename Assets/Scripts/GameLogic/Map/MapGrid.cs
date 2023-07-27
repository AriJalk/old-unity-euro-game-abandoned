using System;

namespace EDBG.MapSystem
{
    public class MapGrid : GridContainer
    {
        public MapGrid() : base(4,4)
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
                    MapTile tile = new MapTile(new GamePosition(i,j,Rows,Columns));
                    //Set quadrants
                    for(int k = 0; k < tile.Rows; k++)
                    {
                        for(int l = 0; l < tile.Columns; l++)
                        {
                            Random rnd=new Random();
                            switch(rnd.Next(0, 3))
                            {
                                case 0:

                                case 1:

                                default:
                                    break;
                            }
                            MapLocation location = (MapLocation)tile.GetCell(k,l);
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
    }
}


