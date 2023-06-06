using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int LandscapeZoom;
    public int PortraitZoom;
    private Camera gameCamera;
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
        gameCamera = transform.Find("MainCamera").GetComponent<Camera>();

        UpdateAspectRatio(Screen.width, Screen.height, ScreenOrientation.LandscapeLeft);
    }

    public void UpdateAspectRatio(int width, int height, ScreenOrientation screenOrientation)
    {
        float currentAspectRatio = (width > height) ? width / height : height / width;
        float targetAspectRatio = 16f / 9f;
        float scaleFactor = currentAspectRatio / targetAspectRatio;
        float orthographicSize;
        Vector3 position;
        ScreenOrientation orientation;
        if (width > height)
        {
            orientation = ScreenOrientation.LandscapeLeft;
            position = new Vector3(3.5f, 2.5f, 0.5f);
        }

        else
        {
            orientation = ScreenOrientation.Portrait;
            position = new Vector3(3, 2, 0);
        }

        // Calculate the orthographic size based on the desired zoom level and aspect ratio
        if (currentAspectRatio >= targetAspectRatio)
        {
            if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
            {
                orthographicSize = LandscapeZoom * scaleFactor;
            }
            else
            {
                orthographicSize = PortraitZoom * scaleFactor;
            }

        }
        else
        {
            if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
            {
                orthographicSize = LandscapeZoom / scaleFactor;
            }
            else
            {
                orthographicSize = PortraitZoom / scaleFactor;
            }
        }
        if (orthographicSize >0)
            gameCamera.orthographicSize = orthographicSize;
        else Debug.LogError("Invalid camera size");
        transform.position = position;
    }

    public void MoveCamera(float horizontal, float vertical)
    {
        float panSpeed = 10f;
        float newX = horizontal * panSpeed * Time.deltaTime;
        float newZ = vertical * panSpeed * Time.deltaTime;
        transform.Translate(new Vector3(newX, 0, newZ), Space.World);
    }
}
