using System;
/// <summary>
/// Base class for player, human or automata
/// </summary>
public abstract class PlayerBase : ICloneable
{
    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private CorporationBase _corporation;

    public CorporationBase Corporation
    {
        get { return _corporation; }
        set { _corporation = value; }
    }


    private int _discStock;

    public int DiscStock
    {
        get { return _discStock; }
        set
        {
            _discStock = value;
        }
    }

    private int _buildPoints;

    public int BuildPoints
    {
        get { return _buildPoints; }
        set { _buildPoints = value; }
    }

    private int _disruptPoints;

    public int DisruptPoints
    {
        get { return _disruptPoints; }
        set { _disruptPoints = value; }
    }


    public PlayerBase(string name, int discStock, CorporationBase corporation)
    {
        _name = name;
        _discStock = discStock;
        _corporation = corporation;
    }

    private PlayerBase(PlayerBase other)
    {
        _name = other._name;
        _discStock = other._discStock;
        _corporation = other._corporation;
    }

    public virtual object Clone()
    {
        PlayerBase clone = (PlayerBase)this.MemberwiseClone();
        clone._name = this._name;
        clone._discStock = this._discStock;
        clone._corporation = this._corporation;
        return clone;
    }
}
