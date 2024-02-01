using EDBG.GameLogic.Rules;

namespace EDBG.Commands
{
    public abstract class CommandBase
    {
        public Player ActivePlayer { get; protected set; }

        public CommandBase(Player activePlayer) 
        { 
            ActivePlayer = activePlayer;
        }
        public abstract void ExecuteCommand();
        public abstract void UndoCommand();
    }
}