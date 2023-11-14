using EDBG.GameLogic.Core;
using EDBG.UserInterface;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDBG.States
{
    public class GameState : ICloneable
    {
        public GameLogicState GameLogicState { get; private set; }

        private GameState(GameState other)
        {
            GameLogicState = other.GameLogicState.Clone() as GameLogicState;
        }

        public GameState(GameLogicState logicState)
        {
            GameLogicState = logicState;
        }

        public object Clone()
        {
            return new GameState(this);
        }
    }
}