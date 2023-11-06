
using EDBG.UserInterface;
using UnityEngine;

namespace EDBG.GameLogic.GameStates
{
    public class ChooseAction : IGameState
    {
        private GameUI gameUI;
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
            gameUI = obj as GameUI;
        }

        public object Exit()
        {
            return null;
        }

        public void Update()
        {
            return;
        }

        public void Update(object obj)
        {
            DieObject die = obj as DieObject;
        }
    }
}