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
        public enum UIElements
        {
            HumanPlayerActions,
            UndoButton,
            ConfirmButton,
        }
        //UI panels
        private Transform actionPanel;
        private Transform gameCommands;
        //UI buttons
        private UIAction confirmAction;
        private UIAction undoAction;

        private GameManager gameManager;
        private UIEvents uiEvents;


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
            actionPanel = transform.Find("ActionPanel");
            gameCommands = transform.Find("StatusBar").Find("GameCommands");
            undoAction = gameCommands.Find("UndoAction").GetComponent<UIAction>();
            confirmAction = gameCommands.Find("ConfirmAction").GetComponent<UIAction>();

            confirmAction.Button.onClick.AddListener(delegate { ActionClicked(confirmAction); });
            undoAction.Button.onClick.AddListener(delegate { ActionClicked(undoAction); });

            uiEvents = new UIEvents();
            BuildInfo();
        }

        //TODO: dynamic action prefab in ui
        public void BuildActions()
        {

        }

        public void BuildInfo()
        {
            Transform playerPanel = transform.Find("PlayerPanel");
            if (playerPanel != null)
            {
                Transform test = playerPanel.Find("DiscStock").Find("DiscCount");
                if (test != null)
                {
                    Debug.Log("Found");
                }
                TextMeshProUGUI discStockText = playerPanel.Find("DiscStock").Find("DiscCount").GetComponent<TextMeshProUGUI>();
                if (discStockText != null)
                {
                    Debug.Log("Text Found");
                }

                discStockText.SetText(gameManager.StateManager.CurrentState.GameLogicState.PlayerList[0].DiscStock.ToString());

            }
        }

        public void SetElementLock(UIElements element, bool isLocked)
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

        private void ActionClicked(UIAction action)
        {
            uiEvents.SelectAction(action);
        }

    }
}