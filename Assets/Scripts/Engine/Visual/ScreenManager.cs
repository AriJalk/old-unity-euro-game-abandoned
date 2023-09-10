using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;
    public static ScreenManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Try to find an existing instance in the scene
                instance = FindObjectOfType<ScreenManager>();

                if (instance == null)
                {
                    // If no instance exists, create a new GameObject with PoolManager and attach it
                    GameObject managerObject = new GameObject("ScreenManager");
                    instance = managerObject.AddComponent<ScreenManager>();
                }

                // Ensure the instance persists across scenes
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

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
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            aspectRatio = Screen.width / Screen.height;
            screenOrientation = Screen.orientation;
            ScreenChanged?.Invoke(this, new ScreenChangedEventArgs(screenWidth, screenHeight, screenOrientation));
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
