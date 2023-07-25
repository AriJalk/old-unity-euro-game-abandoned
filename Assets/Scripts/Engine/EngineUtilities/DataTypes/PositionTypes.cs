public struct GamePosition
{
    private int _x;
    private int _y;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    public readonly int MaxX;
    public readonly int MaxY;

    public GamePosition(int x, int y, int xmax, int ymax)
    {
        _x = x;
        _y = y;
        MaxX = xmax;
        MaxY = ymax;
    }

    public bool Move(int deltaX, int deltaY)
    {
        if (X + deltaX > MaxX || Y + deltaY > MaxY || X + deltaX < 0 || Y + deltaY < 0)
            return false;
        _x += deltaX;
        _y += deltaY;
        return true;
    }

    public bool SetPosition(int x, int y)
    {
        if (x > MaxX || y > MaxY || x < 0 || y < 0)
            return false;
        _x = x;
        _y = y;
        return true;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
