using EDBG.States;
using EDBG.UserInterface;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDBG.GameLogic.Core
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

        public StateManager()
        {
            _stateStack = new Stack<GameState>();
        }

        public void NextState()
        {
            if(_stateStack != null)
            {
                _stateStack.Push(CurrentState.Clone() as GameState);
            }
        }

        public void NextState(GameLogicState state)
        {
            _stateStack.Push(new GameState(state));
        }

        public void UndoState()
        {
            if( _stateStack.Count > 1 ) 
            {
                _stateStack.Pop();
            }
        }
    }
}
