using EDBG.UserInterface;
using System;

namespace EDBG.States
{
    public interface IUIState : ICloneable
    {
        string Name { get; }
        void SetUI(GameUI ui);
    }
}