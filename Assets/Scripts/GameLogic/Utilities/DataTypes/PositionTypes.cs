namespace EDBG.Utilities.DataTypes
{
    public struct GamePosition
    {
        private int _row;
        private int _col;

        public int Row { get { return _row; } }
        public int Col { get { return _col; } }


        public GamePosition(int row, int col)
        {
            _row = row;
            _col = col;
        }

        public void Move(int deltaRow, int deltaCol)
        {
            _row += deltaRow;
            _col += deltaCol;
        }

        public void SetPosition(int row, int col)
        {
            _row = row;
            _col = col;
        }

        public override string ToString()
        {
            return $"({_row}, {_col})";
        }

        public override bool Equals(object obj)
        {
            GamePosition other = (GamePosition)obj;
            return Row == other.Row && Col == other.Col;

        }
    }

    public enum Direction { Left, Right, Up, Down, UpLeft, DownLeft, UpRight, DownRight, Neutral }
}