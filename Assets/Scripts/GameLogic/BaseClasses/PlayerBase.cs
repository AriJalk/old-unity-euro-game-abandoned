using System;
/// <summary>
/// Base class for player, human or Bot
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

    private int _expansionPoints;

    public int ExpansionPoints
    {
        get { return _expansionPoints; }
        set { _expansionPoints = value; }
    }

    private int _marketPoints;

    public int MarketPoints
    {
        get { return _marketPoints; }
        set { _marketPoints = value; }
    }


    public PlayerBase(string name, int discStock, CorporationBase corporation)
    {
        _name = name;
        _discStock = discStock;
        _corporation = corporation;
        _expansionPoints = 0;
        _marketPoints = 0;
    }

    private PlayerBase(PlayerBase other)
    {
        _name = other._name;
        _discStock = other._discStock;
        _corporation = other._corporation;
        _expansionPoints = other._expansionPoints;
        _marketPoints = other._marketPoints;
    }

    public virtual object Clone()
    {
        PlayerBase clone = (PlayerBase)this.MemberwiseClone();
        clone._name = this._name;
        clone._discStock = this._discStock;
        clone._corporation = this._corporation;
        clone._expansionPoints = this._expansionPoints;
        clone._marketPoints = this._marketPoints;
        return clone;
    }
}
