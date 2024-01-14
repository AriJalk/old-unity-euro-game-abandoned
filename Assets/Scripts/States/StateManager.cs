using EDBG.States;
using EDBG.UserInterface;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDBG.States
{
    public class StateManager
    {
        private Stack<LogicState> _stateStack;

        public LogicState CurrentState
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
            _stateStack = new Stack<LogicState>();
        }

        public void PushState(LogicState state)
        {
            _stateStack.Push(state);
        }

        public void PushCurrentState()
        {
            _stateStack.Push(CurrentState.Clone() as LogicState);
        }

        public LogicState PopState()
        {
            return _stateStack.Pop();
        }
    }
}
