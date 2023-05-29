using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Disc
{
    public enum DiscColorPalette
    {
        Blue,
        Red,
        Green,
        White
    }

    public DiscColorPalette DiscColor
    {
        get; private set;
    } 

    public Disc(Disc otherDisc)
    {
        DiscColor = otherDisc.DiscColor;
    }

    public Disc(DiscColorPalette discColor)
    {
        DiscColor = discColor;
    }


}
