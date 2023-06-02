using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
        UpdateAspectRatio();
    }

    public void UpdateAspectRatio()
    {
        float width = Screen.width;
        float height = Screen.height;
        float aspectRatio = (width > height) ? width / height : height / width;
        // Set the desired zoom level for the camera
        float desiredZoom = 7f; // Adjust this value to your preference

        // Calculate the orthographic size based on the desired zoom level and aspect ratio
        float orthographicSize = desiredZoom / aspectRatio;

        // Set the orthographic size of the camera
        gameCamera.orthographicSize = orthographicSize;
    }

    public void MoveCamera(float horizontal, float vertical)
    {
        float panSpeed = 10f;
        float newX = horizontal * panSpeed * Time.deltaTime;
        float newZ = vertical * panSpeed * Time.deltaTime;
        transform.Translate(new Vector3(newX, 0, newZ), Space.World);
    }
}
