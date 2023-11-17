using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastFromRawImage : MonoBehaviour, IPointerClickHandler
{
    public RawImage rawImage;
    public Camera raycastingCamera; // The camera you want to cast rays from
    public LayerMask layerMask;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Calculate the local position within the RectTransform
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, eventData.position, null, out localPosition);

        // Calculate the relative coordinates within the image (0 to 1)
        float relativeX = (localPosition.x - rawImage.rectTransform.rect.x) / rawImage.rectTransform.rect.width;
        float relativeY = (localPosition.y - rawImage.rectTransform.rect.y) / rawImage.rectTransform.rect.height;

        // Create a Vector3 in viewport space
        Vector3 cameraPoint = new Vector3(relativeX, relativeY, 0f);

        // Log the relative coordinates
        Debug.Log($"Relative Coordinates (X): {relativeX}, (Y): {relativeY}");
        Debug.Log($"Camera Point: {cameraPoint}");

        // Cast rays using the desired raycasting camera
        Ray ray = raycastingCamera.ViewportPointToRay(cameraPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log($"Ray Hit: {hit.collider.gameObject.name}");
        }

        // Draw a red debug line
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
    }
}
