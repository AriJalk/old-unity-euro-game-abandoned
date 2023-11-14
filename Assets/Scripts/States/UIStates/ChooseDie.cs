using EDBG.GameLogic.Rules;
using EDBG.UserInterface;

namespace EDBG.States
{
    public class ChooseDie : IUIState
    {
        public string Name => "ChooseDie";

        private ChooseDie(ChooseDie other)
        {

        }

        public ChooseDie()
        {

        }

        public object Clone()
        {
            return new ChooseDie(this);
        }

        public void SetUI(GameUI ui)
        {
            foreach(UIAction action in ui.HumanActions)
            {
                action.Button.interactable = false;
            }
        }
    }
}