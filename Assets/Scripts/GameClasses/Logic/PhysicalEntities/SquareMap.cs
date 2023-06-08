using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDBG.MapSystem
{
    public class SquareMap
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

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
        private SquareTile[,] _tileGrid;

        public SquareTile[,] TileGrid
        {
            get
            {
                return _tileGrid;
            }
        }




        //Constructors

        public SquareMap()
        {
            _tileGrid = new SquareTile[maxHeight, maxWidth];
            _rows = 4;
            _columns = 4;
            TestBuild();
        }

        public SquareMap(SquareMap other)
        {
            this._rows = other.Rows;
            this._columns = other.Columns;
            this._tileGrid = other.TileGrid;
        }

        public SquareMap(int maxHeight, int maxWidth)
        {
            _tileGrid = new SquareTile[maxHeight, maxWidth];
        }

        public void SetGrid(SquareTile[,] tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public void SetTile(int x, int y, SquareTile tile)
        {
            TileGrid[x, y] = tile;
        }

        public SquareTile GetTile(int x, int y)
        {
            return TileGrid[x, y];
        }

        private void TestBuild()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {

                    SquareTile tile = new SquareTile(i,j);
                    tile.discStack.PushItem(new Disc(Disc.DiscColorPalette.Red));
                    tile.discStack.PushItem(new Disc(Disc.DiscColorPalette.Green));
                    tile.discStack.PushItem(new Disc(Disc.DiscColorPalette.White));
                    tile.discStack.PushItem(new Disc(Disc.DiscColorPalette.Blue));
                    //Set quadrants
                    for(int k = 0; k < 2; k++)
                    {
                        for(int l = 0; l < 2; l++)
                        {
                            tile.DiscStacks[k,l].PushItem(new Disc(Disc.DiscColorPalette.Red));
                            tile.DiscStacks[k, l].PushItem(new Disc(Disc.DiscColorPalette.Green));
                            tile.DiscStacks[k, l].PushItem(new Disc(Disc.DiscColorPalette.White));
                            tile.DiscStacks[k, l].PushItem(new Disc(Disc.DiscColorPalette.Blue));
                        }
                    }

                    SetTile(i, j, tile);
                }
            }
        }

        public SquareTile GetNeighbor(SquareTile tile, Direction direction)
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

            int neighborX = tile.Row + offsetX;
            int neighborY = tile.Column + offsetY;

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


