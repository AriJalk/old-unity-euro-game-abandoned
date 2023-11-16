using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace EDBG.States
{
    public enum UIElements
    {
        UndoButton,
        ConfirmButton,
        DiceSelection,
    }
    public class UIState : ICloneable
    {
        private readonly Dictionary<UIElements, bool> elementLock;

        public UIState(params UIElements[] unlockedElements)
        {
            elementLock = new Dictionary<UIElements, bool>();
            foreach(UIElements element in Enum.GetValues(typeof(UIElements)))
            {
                elementLock[element] = unlockedElements.Contains(element);
            }
        }

        private UIState(UIState other)
        {
            elementLock = new Dictionary<UIElements, bool>(other.elementLock);
        }

        public object Clone()
        {
            return new UIState(this);
        }
    }
}