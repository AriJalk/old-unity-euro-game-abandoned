using UnityEngine;

using EDBG.Engine.Core;

namespace EDBG.Engine.InputManagement
{
    public class InputHandler
    {
        private InputEvents inputEvents;


        public InputHandler()
        {
            inputEvents = GameEngineManager.Instance.InputEvents;
        }

        public void Listen()
        {
            //Get mouse movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0 || verticalInput != 0)
            {
                //Use raw axis to make opposites neutral
                if (Input.GetAxisRaw("Horizontal") == 0)
                    horizontalInput = 0;
                if (Input.GetAxisRaw("Vertical") == 0)
                    verticalInput = 0;
                inputEvents.AxisChanged(new Vector2(horizontalInput, verticalInput));
            }
            bool[] mouseButtons = new bool[2];
            mouseButtons[0] = Input.GetMouseButtonDown(0);
            mouseButtons[1] = Input.GetMouseButtonDown(1);
            if (mouseButtons[0] || mouseButtons[1])
            {
                inputEvents.MouseClicked(mouseButtons, Input.mousePosition);
            }
            Vector2 scroll = Input.mouseScrollDelta;

            if (scroll != Vector2.zero)
            {
                inputEvents.MouseScrolled(scroll.y);
            }
        }
    }

}
