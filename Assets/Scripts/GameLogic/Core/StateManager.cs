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

        /// <summary>
        /// Used for initialization
        /// </summary>
        /// <param name="state"></param>
        public void NextState(GameState state)
        {
            if(_stateStack != null)
            {
                _stateStack.Push(state);
            }
        }

        public void NextState(IUIState gameState)
        {
            _stateStack.Push(new GameState(CurrentState.GameLogicState.Clone() as LogicState, gameState));
        }

        public void UndoState(GameUI ui)
        {
            if( _stateStack.Count > 1 ) 
            {
                _stateStack.Pop();
                CurrentState.UIState.SetUI(ui);
            }
        }
    }
}
