using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Die
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

	private Names.Colors _color;

	public Names.Colors Color
	{
		get { return _color; }
		set { _color = value; }
	}


	public Die()
	{
		_face = 1;
		_color = Names.Colors.White;
	}

	//Copy Constructor
	public Die(Die die)
	{
		_face = die.Face;
		_color = die.Color;
	}

	public Die(int face, Names.Colors color)
	{
		_face = face;
		_color = color;
	}

	public void Roll()
	{
		_face = Random.Range(1, 6);
	}

	public void ChangeValue(int value)
	{
        _face = ((_face - 1 + value) % 6 + 6) % 6 + 1;
    }

}
