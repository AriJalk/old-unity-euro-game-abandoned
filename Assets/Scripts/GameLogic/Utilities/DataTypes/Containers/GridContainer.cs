using System;

namespace EDBG.Utilities.DataTypes
{
    public abstract class GridContainer : ICloneable
    {
        protected ICell[,] _grid;

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

        public virtual T GetCell<T>(int row, int col) where T: ICell
        {
            if (IsValidCoordinate(row, col))
            {
                return (T)_grid[row, col];
            }
            return default(T);
        }

        public virtual ICell GetCell(int row, int col)
        {
            if(IsValidCoordinate(row, col))
            {
                return _grid[row, col];
            }
            return null;
        }

        public virtual ICell GetCell(GamePosition position)
        {
            if (IsValidCoordinate(position.Row, position.Col))
            {
                return _grid[position.Row, position.Col];
            }
            return null;
        }

        public virtual GridContainer GetCellAsContainer(int row, int col)
        {
            ICell cell = _grid[row, col];
            if (cell is GridContainer container)
            {
                return container;
            }
            return null;
        }


        //TODO: deep copy
        public virtual bool SetCell(ICell cell)
        {
            if (IsValidCoordinate(cell.GamePosition.Row, cell.GamePosition.Col))
            {
                _grid[cell.GamePosition.Row, cell.GamePosition.Col] = cell;
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
            int offsetRow = 0;
            int offsetCol = 0;

            switch (direction)
            {
                case Direction.Up:
                    offsetRow = 1;
                    break;
                case Direction.Down:
                    offsetRow = -1;
                    break;
                case Direction.Left:
                    offsetCol = -1;
                    break;
                case Direction.Right:
                    offsetCol = 1;
                    break;
            }

            int neighborRow = cell.GamePosition.Row + offsetRow;
            int neighborCol = cell.GamePosition.Col + offsetCol;

            if (IsValidCoordinate(neighborRow, neighborCol))
            {
                return _grid[neighborRow, neighborCol];
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