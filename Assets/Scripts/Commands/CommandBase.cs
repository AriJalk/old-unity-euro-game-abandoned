namespace EDBG.Commands
{
    public abstract class CommandBase
    {
        public abstract void ExecuteCommand();
        public abstract void UndoCommand();
    }
}