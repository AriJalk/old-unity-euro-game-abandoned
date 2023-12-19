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

        private GameState(GameState other)
        {
            GameLogicState = other.GameLogicState.Clone() as LogicState;

        }

        public GameState(LogicState logicState)
        {
            GameLogicState = logicState;
        }

        public object Clone()
        {
            return new GameState(this);
        }
    }
}