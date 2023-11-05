namespace EDBG.GameLogic.GameStates
{
    public interface IGameState
    {
        bool CanExit { get; }
        string Name { get; }

        void Enter();
        void Enter(object obj);
        void Update();
        void Update(object obj);
        void Cancel();
        object Exit();
    }
}