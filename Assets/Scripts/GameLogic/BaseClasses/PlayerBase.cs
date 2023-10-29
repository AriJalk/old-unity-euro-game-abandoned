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

    private Corporation _corporation;

    public Corporation Corporation
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

    public PlayerBase(string name, int discStock)
    {
        _name = name;
        _discStock = discStock;
    }

    private PlayerBase(PlayerBase other)
    {
        _name = other.Name;
        _discStock = other.DiscStock;
    }

    public virtual object Clone()
    {
        PlayerBase clone = (PlayerBase)this.MemberwiseClone();
        clone._name = this._name;
        clone._discStock = this._discStock;
        return clone;
    }
}
