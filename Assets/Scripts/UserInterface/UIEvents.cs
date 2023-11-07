using System;

namespace EDBG.UserInterface
{
    public class UIEvents
    {
        public event Action<UIAction> ActionSelected;

        public void SubscribeToAllEvents(Action<UIAction> action)
        {
            ActionSelected += action;
        }

        public void SelectAction(UIAction action)
        {
            ActionSelected?.Invoke(action);
        }
    }
}