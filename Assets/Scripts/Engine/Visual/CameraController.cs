using UnityEngine;

namespace EDBG.Engine.Visual
{
    //TODO: move the GameObjects
    public class CameraController : MonoBehaviour
    {
        const float targetAspectRatio = 16f / 9f;

        public int LandscapeZoom;
        public int PortraitZoom;
        private Camera gameCamera;



        [Range(0.5f, 10.0f)]
        public float PanAcceleration = 4;

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
            gameCamera = GetComponent<Camera>();

            UpdateAspectRatio(Screen.width, Screen.height);

        }

        public void UpdateAspectRatio(int width, int height)
        {
            float currentAspectRatio = (width > height) ? width / height : height / width;
            float scaleFactor = currentAspectRatio / targetAspectRatio;
            float orthographicSize;
            Vector3 position;
            ScreenOrientation orientation;
            if (width > height)
            {
                orientation = ScreenOrientation.LandscapeLeft;
                position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            else
            {
                orientation = ScreenOrientation.Portrait;
                position = new Vector3(1.85f, 2, 0);
                gameCamera.pixelRect = new Rect(0, 0, width, height);
            }

            // Calculate the orthographic size based on the desired zoom level and aspect ratio
            if (currentAspectRatio >= targetAspectRatio)
            {
                orthographicSize =
                    (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight ? LandscapeZoom : PortraitZoom) * scaleFactor;
            }
            else
            {
                orthographicSize =
                    (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight ? LandscapeZoom : PortraitZoom) / scaleFactor;
            }
            if (orthographicSize > 0)
                gameCamera.orthographicSize = orthographicSize;
            else Debug.LogError("Invalid camera size");
            transform.position = position;
        }

        public void MoveCamera(float horizontal, float vertical)
        {

            float panSpeed = gameCamera.orthographicSize * PanAcceleration;
            float newX = horizontal * panSpeed * Time.deltaTime;
            float newZ = vertical * panSpeed * Time.deltaTime;
            if (transform.localPosition.x + newX > 4 || transform.localPosition.x + newX < 0)
                newX = 0;
            if (transform.localPosition.y + newZ > 4 || transform.localPosition.y + newZ < 1)
                newZ = 0;
            transform.Translate(new Vector3(newX, newZ, newX), Space.World);


        }

        public void ZoomCamera(float deltaY)
        {
            if (gameCamera.orthographicSize - deltaY >= 0 && gameCamera.orthographicSize - deltaY <= 4)
            {
                gameCamera.orthographicSize -= deltaY;

            }
        }
    }
}
