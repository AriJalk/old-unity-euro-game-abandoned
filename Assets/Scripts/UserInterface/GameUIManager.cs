using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//TODO: middle class between inputhandler and gameengine

public class GameUIManager : MonoBehaviour
{
    private Transform canvas;
    private GameEngineManager gameEngineManager;
    private UILockManager lockManager = new();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string value)
    {

    }

    public void Initialize(GameEngineManager manager)
    {
        gameEngineManager = manager;

    }


    public void AxisChanged(float horizonalInput, float verticalInput)
    {
        //gameEngineManager.MoveCamera(horizonalInput, verticalInput);
    }

    public void MouseClicked(bool[] mouseButtons, Vector3 mousePos)
    {

    }

    public void ScreenChanged(ScreenOrientation orientation)
    {
        if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
        {

        }
        else if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
        {

        }
    }

    public void MouseScrolled(float deltaY)
    {

    }
}
