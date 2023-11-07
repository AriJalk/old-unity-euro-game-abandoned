using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;
using EDBG.UserInterface;
using TMPro;

namespace EDBG.GameLogic.GameStates
{
    public class ChooseAction : IGameState
    {
        private GameUI gameUI;
        private DieObject chosenDie;
        private GameAction chosenAction;
        public bool CanExit { get; private set; }

        private string _name = "ChooseAction";
        public string Name { get { return _name; } }

        public void Cancel()
        {
            return;
        }

        public void Enter()
        {
            return;
        }

        public void Enter(object obj)
        {
            if(obj is GameUI ui)
            {
                gameUI = ui;
                gameUI.SetElementLock(GameUI.UIElements.PlayerActions, true);
                TextMeshProUGUI text = gameUI.transform.Find("StatusBar").GetComponentInChildren<TextMeshProUGUI>();
                text.text = "Choose die from the dice tray";
            }
        }

        public object Exit()
        {
            return chosenAction;
        }

        public void Update(object o)
        {
            if (o is DieObject die)
            {
                if (chosenDie != null)
                    chosenDie.Highlight.gameObject.SetActive(false);
                chosenDie = die;
                chosenDie.Highlight.gameObject.SetActive(true);
                foreach (UIAction action in gameUI.HumanActions)
                {
                    if (action.GameAction.DieFace == die.Die.Result)
                    {
                        action.Button.interactable = true;
                    }
                    else
                    {
                        action.Button.interactable = false;
                    }
                }
                gameUI.StatusText.text = "Choose action from panel or choose another die";
            }
            else if(o is UIAction action)
            {
                chosenAction = action.GameAction;
                CanExit = true;
            }
        }

        public void Update()
        {
            return;
        }
    }
}