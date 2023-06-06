using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChangedEventArgs : EventArgs
{
    public int NewWidth;
    public int NewHeight;
    public ScreenOrientation NewScreenOrientation;

    public ScreenChangedEventArgs(int width, int height,ScreenOrientation screenOrientation)
    {
        NewWidth = width;
        NewHeight = height;
        NewScreenOrientation = screenOrientation;
    }
}
