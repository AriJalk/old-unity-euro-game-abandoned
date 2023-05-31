using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private UserInterfaceObject userInterface;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(UserInterfaceObject ui)
    {
        userInterface = ui;
    }

    public void Listen()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
            userInterface.AxisChanged(horizontalInput, verticalInput);
        bool[] mouseButtons = new bool[2];
        mouseButtons[0] = Input.GetMouseButton(0);
        mouseButtons[1] = Input.GetMouseButton(1);
        if (mouseButtons[0] || mouseButtons[1])
        {
            userInterface.MouseClicked(mouseButtons, Input.mousePosition);
        }
        
    }
}
