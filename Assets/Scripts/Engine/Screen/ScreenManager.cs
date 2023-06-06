using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenManager : MonoBehaviour
{
    public EventHandler<ScreenChangedEventArgs> ScreenChanged;


    private int screenWidth;
    private int screenHeight;
    private float aspectRatio;
    private ScreenOrientation screenOrientation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height || aspectRatio != (float)Screen.width / Screen.height || screenOrientation != Screen.orientation)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            aspectRatio = Screen.width / Screen.height;
            screenOrientation = Screen.orientation;
            ScreenChanged?.Invoke(this, new ScreenChangedEventArgs(screenWidth, screenHeight, Screen.orientation));
        }
    }

    public void Initialize()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        aspectRatio = (float)screenWidth / screenHeight;
        screenOrientation = Screen.orientation;
    }
}
