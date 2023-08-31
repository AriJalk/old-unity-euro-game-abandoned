using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EDBG.Rules;

public class Die : IGameComponent
{
	private int _face;

	public int Face
	{
		get { return _face; }
		set
		{
			if (value >= 1 && value <= 6)
			{
				_face = value;
			}
			else
			{
				Debug.LogWarning("Die value not in range 1-6");
			}
		}
	}

	private Colors _color;

	public Colors Color
	{
		get { return _color; }
		set { _color = value; }
	}


	public Die()
	{
		_face = 1;
		_color = Colors.White;
	}

	//Copy Constructor
	private Die(Die die)
	{
		_face = die.Face;
		_color = die.Color;
	}

	public Die(int face, Colors color)
	{
		_face = face;
		_color = color;
	}

	public int Roll()
	{
		_face = Random.Range(1, 6);
		return _face;
	}

	public void ChangeValue(int value)
	{
        _face = ((_face - 1 + value) % 6 + 6) % 6 + 1;
    }

    public override object Clone()
    {
		return new Die(this);
    }
}
