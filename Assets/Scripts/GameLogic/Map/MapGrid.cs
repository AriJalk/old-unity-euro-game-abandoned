using System;
using System.Net.Http.Headers;

namespace EDBG.MapSystem
{
    public class MapGrid
    {
        private const int maxHeight = 100;
        private const int maxWidth = 100;
        private int _rows;
        private int _columns;
        public int Rows
        {
            get
            {
                return _rows;
            }
        }
        public int Columns
        {
            get 
            { 
                return _columns;
            }
        }

        //Private Fields
        private MapTile[,] _tileGrid;

        public MapTile[,] TileGrid
        {
            get
            {
                return _tileGrid;
            }
        }




        //Constructors

        public MapGrid()
        {
            _tileGrid = new MapTile[maxHeight, maxWidth];
            _rows = 4;
            _columns = 4;
            TestBuild();
        }

        public MapGrid(MapGrid other)
        {
            _rows = other.Rows;
            _columns = other.Columns;
            _tileGrid = other.TileGrid;
        }

        public MapGrid(int maxHeight, int maxWidth)
        {
            _tileGrid = new MapTile[maxHeight, maxWidth];
        }

        public void SetGrid(MapTile[,] tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public void SetTile(int x, int y, MapTile tile)
        {
            TileGrid[x, y] = tile;
        }

        public MapTile GetTile(int x, int y)
        {
            return TileGrid[x, y];
        }

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

                            tile.InnerGrid[k,l].DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                            tile.InnerGrid[k, l].DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                            tile.InnerGrid[k, l].DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                            tile.InnerGrid[k, l].DiscStack.PushItem(new Disc(UtilityFunctions.GetRandomComponentColor()));
                        }
                    }

                    SetTile(i, j, tile);
                }
            }
        }

        public MapTile GetNeighbor(MapTile tile, Direction direction)
        {
            int offsetX = 0;
            int offsetY = 0;

            switch (direction)
            {
                case Direction.Up:
                    offsetY = 1;
                    break;
                case Direction.Down:
                    offsetY = -1;
                    break;
                case Direction.Left:
                    offsetX = -1;
                    break;
                case Direction.Right:
                    offsetX = 1;
                    break;
            }

            int neighborX = tile.GamePosition.X + offsetX;
            int neighborY = tile.GamePosition.Y + offsetY;

            if (IsValidCoordinate(neighborX, neighborY))
            {
                return TileGrid[neighborX, neighborY];
            }

            return null;
        }

        private bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 && x < Rows && y >= 0 && y < Columns;
        }
    }
}


