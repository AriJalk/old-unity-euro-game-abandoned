
using System;
using System.Collections.Generic;

public abstract class CorporationBase : ICloneable
{
	private string _name;

	public string Name
	{
		get { return _name; }
		set { _name = value; }
	}

	private string _description;

	public string Description
	{
		get { return _description; }
		set { _description = value; }
	}


	private GameActionBase[] _actions;

	public GameActionBase[] Actions
	{
		get { return _actions; }
		set { _actions = value; }
	}



	public object Clone()
    {
        CorporationBase clone = (CorporationBase)this.MemberwiseClone();
		clone._name = this._name;
		clone._description = this._description;
		return clone;
    }
}
