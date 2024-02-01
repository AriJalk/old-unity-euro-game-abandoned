using UnityEngine;
using UnityEngine.UI;

using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

using TMPro;
using System.Collections.Generic;
using System.Linq;
using EDBG.GameLogic.Core;
using EDBG.Engine.ResourceManagement;

namespace EDBG.UserInterface
{
    public class GameUI : MonoBehaviour
    {
        //UI panels
        private Transform actionPanel;
        private Transform gameCommands;
        //UI buttons
        private UIAction confirmAction;
        private UIAction undoAction;

        private GameManager gameManager;


        public TextMeshProUGUI StatusText;
        public UIAction UIActionPrefab;


        void Start()
        {

        }

        private void Update()
        {

        }

        public void Initialize(GameManager manager)
        {
            gameManager = manager;

        }


        public void SetElementLock(bool isLocked, UIElements element)
        {
            switch (element)
            {
                case UIElements.UndoButton:
                    undoAction.Button.interactable = !isLocked;
                    break;
                case UIElements.ConfirmButton:
                    confirmAction.Button.interactable = !isLocked;
                    break;
                default:
                    break;
            }
        }

        public void SetElementLock(bool isLocked, params UIElements[] elements)
        {
            foreach(UIElements element in elements)
            {
                switch (element)
                {
                    case UIElements.UndoButton:
                        undoAction.Button.interactable = !isLocked;
                        break;
                    case UIElements.ConfirmButton:
                        confirmAction.Button.interactable = !isLocked;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}