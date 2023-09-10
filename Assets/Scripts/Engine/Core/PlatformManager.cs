using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        // Call platform-specific methods or perform platform-specific actions here
        if (Application.platform == RuntimePlatform.Android)
        {
            HandleAndroidPlatform();
        }
        else if (Application.isEditor)
        {
            HandleEditorPlatform();
        }
        else
        {
            HandleOtherPlatform();
        }
    }
    private void HandleAndroidPlatform()
    {
        // Actions specific to Android platform
        Debug.Log("Running on Android");
    }

    private void HandleEditorPlatform()
    {
        // Actions specific to Unity Editor
        Debug.Log("Running in the Unity Editor");
    }

    private void HandleOtherPlatform()
    {
        // Actions specific to other platforms
        Debug.Log("Running on a different platform");
    }
}
