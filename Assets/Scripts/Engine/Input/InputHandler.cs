using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
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

    }

    public void Listen(GameEngineManager manager)
    {
        const float panDistance = 10f;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float x = horizontalInput * panDistance * Time.deltaTime;
        float z = verticalInput * panDistance * Time.deltaTime;
        manager.MoveCamera(new Vector3(x, 0, z));
        if (Input.GetMouseButtonDown(0))
        {
            var position = Input.mousePosition;
            manager.SelectObject(position);
        }
    }
}
