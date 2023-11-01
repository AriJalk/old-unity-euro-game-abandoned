using System;
using UnityEngine;

namespace EDBG.Engine.Visual
{
    public class ScreenChangedEventArgs : EventArgs
    {
        public int NewWidth;
        public int NewHeight;
        public ScreenOrientation NewScreenOrientation;

        public ScreenChangedEventArgs(int width, int height, ScreenOrientation screenOrientation)
        {
            NewWidth = width;
            NewHeight = height;
            NewScreenOrientation = screenOrientation;
        }
    }

}
