using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    private ScreenOrientation currentOrientation;
    private GameEngineManager gameEngineManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation != currentOrientation)
        {
            // Screen orientation has changed
            // Perform the necessary actions or logic here

            // Update the previous orientation to the current one
            currentOrientation = Screen.orientation;
            gameEngineManager.ChangeOrientation(currentOrientation);
        }
    }

    public void Initialize(GameEngineManager gameEngineManager)
    {
        this.gameEngineManager = gameEngineManager;
        currentOrientation = Screen.orientation;
    }
}
