using EDBG.GameLogic.Core;
using EDBG.UserInterface;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDBG.States
{
    public class GameState : ICloneable
    {
        public LogicState GameLogicState { get; private set; }
        public IUIState UIState { get; private set; }

        private GameState(GameState other)
        {
            GameLogicState = other.GameLogicState.Clone() as LogicState;
            UIState = other.UIState.Clone() as IUIState;
        }

        public GameState(LogicState logicState, IUIState uiState)
        {
            GameLogicState = logicState;
            UIState = uiState;
        }

        public object Clone()
        {
            return new GameState(this);
        }
    }
}