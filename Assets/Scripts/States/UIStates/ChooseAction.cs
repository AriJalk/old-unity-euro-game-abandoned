using EDBG.GameLogic.Rules;
using EDBG.UserInterface;

namespace EDBG.States
{
    public class ChooseAction : IUIState
    {
        public DieObject ChosenDie {  get; private set; }
        public string Name => "ChooseAction";

        private ChooseAction(ChooseAction other)
        {
            ChosenDie = other.ChosenDie;
        }

        public ChooseAction(DieObject chosenDie)
        {
            ChosenDie = chosenDie;
        }

        public object Clone()
        {
            return new ChooseAction(this);
        }

        public void SetUI(GameUI ui)
        {
            foreach (UIAction action in ui.HumanActions)
            {
                if (action.DieFace == ChosenDie.Die.Result)
                    action.Button.interactable = true;
                else 
                    action.Button.interactable = false;
            }
        }
    }
}