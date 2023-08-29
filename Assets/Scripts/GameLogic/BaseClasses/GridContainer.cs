using UnityEngine;

namespace EDBG.MapSystem
{
    public abstract class GridContainer
    {
        private ICell[,] _grid;

        public readonly int Rows;

        public readonly int Columns;



        public GridContainer(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _grid= new ICell[Rows, Columns];
        }

        public GridContainer(GridContainer other)
        {
            Rows = other.Rows;
            Columns = other.Columns;
            _grid = other._grid;
        }

        public virtual ICell GetCell(int row, int column)
        {
            if (IsValidCoordinate(row,column)) 
            {
                return _grid[row, column];
            }
            return null;
        }

        public virtual GridContainer GetCellAsContainer(int row, int column)
        {
            ICell cell=_grid[row,column];
            if (cell is GridContainer container)
            {
                return container;
            }
            return null;
        }

        public virtual bool SetCell(ICell cell)
        {
            if (IsValidCoordinate(cell.GamePosition.Y,cell.GamePosition.X))
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

            if (IsValidCoordinate(neighborX, neighborY))
            {
                return _grid[neighborX, neighborY];
            }

            return null;
        }

        private bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 && x < Rows && y >= 0 && y < Columns;
        }
    }
}