using EDBG.GameLogic.Rules;
using EDBG.States;

namespace EDBG.Commands
{
    public abstract class CommandBase
    {
        protected LogicState logicState;
        public RoundStates RoundState { get; private set; }
        public Player ActivePlayer { get; protected set; }
        public bool Result { get; protected set; }

        public CommandBase(LogicState state) 
        { 
            ActivePlayer = state.CurrentPlayer;
            RoundState = state.RoundState;
            logicState = state;
            Result = false;
        }
        public abstract void ExecuteCommand();
        public virtual void UndoCommand()
        {
            logicState.RoundState = RoundState;
            logicState.SetCurrentPlayer(ActivePlayer);
        }
    }
}