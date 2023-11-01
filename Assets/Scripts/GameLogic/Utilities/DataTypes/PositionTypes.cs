namespace EDBG.Utilities.DataTypes
{
    public struct GamePosition
    {
        private int _x;
        private int _y;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }


        public GamePosition(int y, int x)
        {
            _x = x;
            _y = y;
        }

        public void Move(int deltaX, int deltaY)
        {
            _x += deltaX;
            _y += deltaY;
        }

        public void SetPosition(int y, int x)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return $"({Y}, {X})";
        }
    }

    public enum Direction { Left, Right, Up, Down, UpLeft, DownLeft, UpRight, DownRight, Neutral }
}