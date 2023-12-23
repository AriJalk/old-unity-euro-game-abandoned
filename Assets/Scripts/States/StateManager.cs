using EDBG.States;
using EDBG.UserInterface;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDBG.States
{
    public class StateManager
    {
        private Stack<GameState> _stateStack;

        public GameState CurrentState
        {
            get
            {
                if(_stateStack != null && _stateStack.Count > 0)
                    return _stateStack.Peek();
                return null;
            }
        }

        public int Count
        {
            get
            {
                return _stateStack.Count;
            }
        }

        public StateManager()
        {
            _stateStack = new Stack<GameState>();
        }

        public void PushState(GameState state)
        {
            _stateStack.Push(state);
        }

        public void PushCurrentState()
        {
            _stateStack.Push(CurrentState.Clone() as GameState);
        }

        public GameState PopState()
        {
            return _stateStack.Pop();
        }
    }
}
