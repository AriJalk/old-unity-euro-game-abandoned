using EDBG.GameLogic.Components;

namespace EDBG.GameLogic.GameStates
{
    public class ChooseDie : IGameState
    {
        private DieObject chosenDie;
        public bool CanExit { get; private set; }
        private string _name = "ChooseDie";
        public string Name { get { return _name; } private set { _name = value; } }

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
            if(obj is GameUI gameUI)
            {
                
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
                chosenDie = die;
                CanExit = true;
            }
        }

        public void Update()
        {
            return;
        }
    }
}