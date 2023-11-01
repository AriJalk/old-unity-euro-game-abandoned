using System;

namespace EDBG.Utilities.DataTypes
{
    public abstract class GridContainer : ICloneable
    {
        private ICell[,] _grid;

        public readonly int Rows;

        public readonly int Columns;



        public GridContainer(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _grid = new ICell[Rows, Columns];
        }

        protected GridContainer(GridContainer other)
        {
            _grid = new ICell[other.Rows, other.Columns];
            Rows = other.Rows;
            Columns = other.Columns;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _grid[i, j] = (ICell)other._grid[i, j].Clone();
                }
            }
        }

        public virtual ICell GetCell(int x, int y)
        {
            if (IsValidCoordinate(y, x))
            {
                return _grid[y, x];
            }
            return null;
        }

        public virtual GridContainer GetCellAsContainer(int x, int y)
        {
            ICell cell = _grid[y, x];
            if (cell is GridContainer container)
            {
                return container;
            }
            return null;
        }


        //TODO: deep copy
        public virtual bool SetCell(ICell cell)
        {
            if (IsValidCoordinate(cell.GamePosition.Y, cell.GamePosition.X))
            {
                _grid[cell.GamePosition.Y, cell.GamePosition.X] = cell;
                return true;
            }
            return false;
        }

        public void SetGrid(ICell[,] grid)
        {
            _grid = grid;
        }

        public ICell GetNeighbor(ICell cell, Direction direction)
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

            int neighborX = cell.GamePosition.X + offsetX;
            int neighborY = cell.GamePosition.Y + offsetY;

            if (IsValidCoordinate(neighborY, neighborX))
            {
                return _grid[neighborY, neighborX];
            }

            return null;
        }

        private bool IsValidCoordinate(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        public abstract object Clone();
    }
}