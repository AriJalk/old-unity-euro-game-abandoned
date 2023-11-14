using UnityEngine;
using UnityEngine.UI;

using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

using TMPro;
using System.Collections.Generic;
using System.Linq;
using EDBG.GameLogic.Core;

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
        private Transform humanActionPanel;
        private Transform botActionPanel;
        private Transform gameCommands;
        //UI buttons
        private UIAction confirmAction;
        private UIAction undoAction;

        private GameManager gameManager;
        private UIEvents uiEvents;
        public UIAction[] HumanActions { get; private set; }
        public UIAction[] BotActions { get; private set; }

        public TextMeshProUGUI StatusText;


        void Start()
        {

        }

        private void Update()
        {
            
        }

        public void Initialize(GameManager manager, UIEvents uIEvents)
        {
            gameManager = manager;
            actionPanel = transform.Find("ActionPanel");
            humanActionPanel = actionPanel.Find("HumanActions");
            botActionPanel = actionPanel.Find("BotActions");
            gameCommands = transform.Find("GameCommands");
            confirmAction = gameCommands.Find("ConfirmAction").GetComponent<UIAction>();
            undoAction = gameCommands.Find("UndoAction").GetComponent<UIAction>();
            confirmAction.Button.onClick.AddListener(delegate { ActionClicked(confirmAction); });
            undoAction.Button.onClick.AddListener(delegate { ActionClicked(undoAction); });
            BuildActions();
            BuildInfo();
            this.uiEvents = uIEvents;
        }

        //TODO: dynamic action prefab in ui
        public void BuildActions()
        {
            HumanPlayer humanPlayer = gameManager.StateManager.CurrentState.GameLogicState.PlayerList[0] as HumanPlayer;
            BotPlayer botPlayer = gameManager.StateManager.CurrentState.GameLogicState.PlayerList[1] as BotPlayer;
            HumanActions = new UIAction[humanPlayer.Corporation.CorpActions.Length];
            if (humanActionPanel != null)
            {
                for (int i = 0; i < humanPlayer.Corporation.CorpActions.Length; i++)
                {
                    UIAction action = humanActionPanel.Find($"Action{i + 1}").GetComponent<UIAction>();
                    HumanActions[i] = action;
                    action.DieFace = humanPlayer.Corporation.CorpActions[i].DieFace;
                    action.TextBox.text = humanPlayer.Corporation.CorpActions[i].Name;
                    action.Button.onClick.AddListener(delegate { ActionClicked(action); });
                }
            }
            BotActions = new UIAction[botPlayer.Corporation.CorpActions.Length];
            if (botActionPanel != null)
            {
                for (int i = 0; i < botPlayer.Corporation.CorpActions.Length; i++)
                {
                    UIAction action = botActionPanel.Find($"Action{i + 1}").GetComponent<UIAction>();
                    BotActions[i] = action;
                    action.DieFace = botPlayer.Corporation.CorpActions[i].DieFace;
                    action.TextBox.text = botPlayer.Corporation.CorpActions[i].Name;
                    action.Button.interactable = false;
                }
            }

        }

        public void BuildInfo()
        {
            Transform playerPanel = transform.Find("PlayerPanel");
            if (playerPanel != null)
            {
                TextMeshProUGUI discStockText = playerPanel.Find("DiscStock").Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI expansionPointsText = playerPanel.Find("ExpansionPoints").Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI marketPointsText = playerPanel.Find("MarketPoints").Find("Text").GetComponent<TextMeshProUGUI>();

                discStockText.SetText($"Discs: {gameManager.StateManager.CurrentState.GameLogicState.PlayerList[0].DiscStock}");
                expansionPointsText.SetText($"EP: {gameManager.StateManager.CurrentState.GameLogicState.PlayerList[0].ExpansionPoints}");
                marketPointsText.SetText($"MP: {gameManager.StateManager.CurrentState.GameLogicState.PlayerList[0].MarketPoints}");

            }
        }

        public void SetElementLock(UIElements element, bool isLocked)
        {
            switch (element)
            {
                case UIElements.HumanPlayerActions:
                    foreach (Transform child in humanActionPanel)
                    {
                        Button button = child.GetComponentInChildren<Button>();
                        button.interactable = !isLocked;
                    }
                    break;
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

        public void SetDieActionLock(int dieFace)
        {
            foreach (UIAction action in HumanActions)
            {
                if (action.DieFace == dieFace)
                {
                    action.Button.interactable = true;
                }
                else
                    action.Button.interactable = false;
            }
        }

        private void ActionClicked(UIAction action)
        {
            uiEvents.SelectAction(action);
        }

    }
}