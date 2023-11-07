using EDBG.GameLogic.Rules;
using EDBG.UserInterface;
using TMPro;

namespace EDBG.GameLogic.GameStates
{
    public class ActionState : IGameState
    {   
        private GameUI gameUI;
        private GameAction selectedAction;
        public bool CanExit { get; private set; }

        private string _name = "ActionState";
        public string Name { get { return _name; } }

        public void Cancel()
        {
            return;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Enter(object obj)
        {

        }


        public void Enter(GameUI gameUI, object obj)
        {
            this.gameUI = gameUI;
            if(obj is GameAction action)
            {
                this.selectedAction = action;
                gameUI.StatusText.text = action.Description;
                gameUI.SetElementLock(GameUI.UIElements.PlayerActions, true);
            }
        }

        public object Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Update(object obj)
        {
            if (obj is GameAction action)
            {

            }
        }

    }
}