using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;
using EDBG.UserInterface;
using UnityEngine;

namespace EDBG.GameLogic.GameStates
{
    public class ChooseActionState : UIGameState
    {
        private DieObject selectedDie;
        private GameAction selectedAction;

        public ChooseActionState(GameUI gameUI) : base(gameUI)
        {

        }

        public override void Enter()
        {
            gameUI.SetElementLock(GameUI.UIElements.PlayerActions, true);
            gameUI.StatusText.text = "Select 1 available die";
        }

        /// <summary>
        /// Receives either die or action
        /// </summary>
        /// <param name="parameters"></param>
        public override void Update(object parameter)
        {
            if (parameter is DieObject die)
            {
                if (selectedDie != null)
                    selectedDie.Highlight.gameObject.SetActive(false);
                selectedDie = die;
                selectedDie.Highlight.gameObject.SetActive(true);
                gameUI.SetActionLock(selectedDie.Die.Result);
                gameUI.StatusText.text = "Select matching action or another die";
            }
            if (parameter is UIAction action)
            {
                selectedAction = action.GameAction;
                CanExit = true;
            }
        }

        public override void Update(params object[] parameters)
        {
            return;
        }


        public override object Exit()
        {
            return selectedAction;
        }
    }
}