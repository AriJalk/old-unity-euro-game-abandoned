using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;
using EDBG.UserInterface;
using TMPro;

namespace EDBG.GameLogic.GameStates
{
    public class ChooseDie : IGameState
    {
        private GameUI gameUI;
        private DieObject chosenDie;
        public bool CanExit { get; private set; }

        private string _name = "ChooseDie";
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
            return chosenDie;
        }

        public void Update(object o)
        {
            if (o is DieObject die)
            {
                if (chosenDie != null)
                    chosenDie.Highlight.gameObject.SetActive(false);
                chosenDie = die;
                chosenDie.Highlight.gameObject.SetActive(true);
                CanExit = true;
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
                TextMeshProUGUI text = gameUI.transform.Find("StatusBar").GetComponentInChildren<TextMeshProUGUI>();
                text.text = "Choose action from panel or choose another die";
            }
        }

        public void Update()
        {
            return;
        }
    }
}