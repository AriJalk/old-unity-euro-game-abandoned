using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Camera Camera;
    public LayerMask LayerMask;


    public Transform Raycast(Vector2 position)
    {
        Ray ray = Camera.ScreenPointToRay(position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 20f, LayerMask))
        {
            Debug.Log(hit.transform.name);
            return hit.transform;
        }
        return null;
    }
}
