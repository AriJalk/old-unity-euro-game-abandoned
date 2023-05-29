using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTile
{
    public enum TileTypeList
    {
        Mountain,
        Desert,
        Forest,
        City,
        Default
    }

    private int _row;

    public int Row
    {
        get { return _row; }
        set { _row = value; }
    }

    private int _column;

    public int Column
    {
        get { return _column; }
        set { _column = value; }
    }


    private TileTypeList _tileType;

    public TileTypeList TileType
    {
        get
        {
            return _tileType;
        }
        set
        {
            _tileType = value;
        }
    }

    public DiscStack discStack;


    public SquareTile(int row, int col)
    {
        TileType = TileTypeList.Default;
        discStack= new DiscStack();
        _row = row;
        _column = col;
    }

    public SquareTile(SquareTile tile)
    {
        TileType = tile.TileType;
        discStack = new DiscStack();
        _row= tile.Row;
        _column= tile.Column;
    }

    public SquareTile(TileTypeList type, int row, int col)
    {
        TileType = type;
        discStack = new DiscStack();
        _row = row;
        _column= col;
    }
}
