using EDBG.UserInterface;

namespace EDBG.GameLogic.GameStates
{
    public abstract class UIGameState
    {
        protected GameUI gameUI;
        public bool CanExit { get; protected set; }
        public UIGameState(GameUI gameUI)
        {
            this.gameUI = gameUI;
        }

        /// <summary>
        /// Call when state is ready to enter
        /// </summary>
        public abstract void Enter();
        public abstract void Update(object parameter);
        public abstract void Update(params object[] parameters);
        public abstract object Exit();
    }
}