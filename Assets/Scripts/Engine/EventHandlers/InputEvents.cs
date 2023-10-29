
using EDBG.Rules;
using System;
using UnityEngine;

public class InputEvents
{
    private event Action<Vector2> axisChanged;
    private event Action<bool[],Vector2> mouseClicked;
    private event Action<float> mouseScrolled;

    // Methods for subscribing to events
    public void SubscribeToAllEvents(Action<Vector2> axisHandler, Action<bool[],Vector2> mouseClickHandler, Action<float> mouseScrollHandler)
    {
        axisChanged += axisHandler;
        mouseClicked += mouseClickHandler;
        mouseScrolled += mouseScrollHandler;
    }

    public void UnsubscribeFromAllEvents(Action<Vector2> axisHandler, Action<bool[], Vector2> mouseClickHandler, Action<float> mouseScrollHandler)
    {
        axisChanged -= axisHandler;
        mouseClicked -= mouseClickHandler;
        mouseScrolled -= mouseScrollHandler;
    }

    public void AxisChanged(Vector2 input)
    {
        axisChanged?.Invoke(input);
    }


    public void MouseClicked(bool[] mouseButtons, Vector2 input)
    {
        mouseClicked?.Invoke(mouseButtons, input);
    }

    public void MouseScrolled(float scrollAmount)
    {
        mouseScrolled?.Invoke(scrollAmount);
    }
}
