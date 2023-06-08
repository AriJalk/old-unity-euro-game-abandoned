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

    private GameStack<Disc>[,] _discStacks;

    public GameStack<Disc>[,] DiscStacks
    {
        get { return _discStacks; }
        set { _discStacks = value; }
    }



    public GameStack<Disc> discStack;


    public SquareTile(int row, int col)
    {
        TileType = TileTypeList.Default;
        discStack = new GameStack<Disc>();
        _row = row;
        _column = col;
        DiscStacks = new GameStack<Disc>[2, 2];
        DiscStacks[0, 0] = new GameStack<Disc>();
        DiscStacks[0, 1] = new GameStack<Disc>();
        DiscStacks[1, 0] = new GameStack<Disc>();
        DiscStacks[1, 1] = new GameStack<Disc>();
    }

    public SquareTile(SquareTile tile)
    {
        TileType = tile.TileType;
        discStack = new GameStack<Disc>();
        _row = tile.Row;
        _column = tile.Column;
        DiscStacks = new GameStack<Disc>[2, 2];
        DiscStacks[0, 0] = new GameStack<Disc>();
        DiscStacks[0, 1] = new GameStack<Disc>();
        DiscStacks[1, 0] = new GameStack<Disc>();
        DiscStacks[1, 1] = new GameStack<Disc>();
    }

    public SquareTile(TileTypeList type, int row, int col)
    {
        TileType = type;
        discStack = new GameStack<Disc>();
        _row = row;
        _column = col;
        DiscStacks = new GameStack<Disc>[2, 2];
        DiscStacks[0, 0] = new GameStack<Disc>();
        DiscStacks[0, 1] = new GameStack<Disc>();
        DiscStacks[1, 0] = new GameStack<Disc>();
        DiscStacks[1, 1] = new GameStack<Disc>();
    }
}
