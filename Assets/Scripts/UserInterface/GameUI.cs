using UnityEngine;
using UnityEngine.UI;

using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

using TMPro;
using System.Collections.Generic;
using System.Linq;

namespace EDBG.UserInterface
{
    public class GameUI : MonoBehaviour
    {
        private UIEvents uiEvents;
        public enum UIElements
        {
            PlayerActions,
        }

        public UIAction[] HumanActions { get; private set; }
        public UIAction[] BotActions { get; private set; }

        public TextMeshProUGUI StatusText;


        void Start()
        {

        }

        public void Initialize(Player humanPlayer, Player botPlayer, UIEvents uIEvents)
        {
            BuildActions(humanPlayer, botPlayer);
            BuildInfo(humanPlayer);
            this.uiEvents = uIEvents;
        }

        public void BuildHand(GameStack<ActionToken> tokenArray)
        {
            Transform playerHand = transform.Find("PlayerHand");
            Vector2 tokenSize = new Vector2(80, 80);
            int size = tokenArray.Count;
            if (size == 0)
                return;
            for (int i = 0; i < size; i++)
            {
                GameObject emptyObject = new GameObject();
                emptyObject.AddComponent<RectTransform>();
                GameObject token = Instantiate(emptyObject, playerHand);

                token.name = "token_" + (i + 1);
                RectTransform rect = token.GetComponent<RectTransform>();
                Vector2 anchorMin = new Vector2(i / (float)size, 0);
                Vector2 anchorMax = new Vector2(i / (float)size + (1 / (float)size), 1);

                rect.anchorMin = anchorMin;
                rect.anchorMax = anchorMax;
                rect.sizeDelta = Vector2.zero;

                GameObject tokenImage = Instantiate(emptyObject, token.transform);
                tokenImage.name = "tokenImage";
                tokenImage.AddComponent<RawImage>();
                Color color = Color.white;
                switch (tokenArray.GetItemByIndex(i).color)
                {
                    case EDBG.GameLogic.Rules.TokenColors.Red:
                        color = Color.red;
                        break;
                    case EDBG.GameLogic.Rules.TokenColors.Green:
                        color = Color.green;
                        break;
                    case EDBG.GameLogic.Rules.TokenColors.Blue:
                        color = Color.blue;
                        break;
                    default:
                        color = Color.white;
                        break;
                }
                tokenImage.GetComponent<RawImage>().color = color;
                tokenImage.GetComponent<RawImage>().texture = (Texture)Resources.Load("Images/Token");
                tokenImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tokenSize.x);
                tokenImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tokenSize.y);

                Destroy(emptyObject);


            }
        }

        //TODO: dynamic action prefab in ui
        public void BuildActions(Player humanPlayer, Player botPlayer)
        {
            HumanActions = new UIAction[humanPlayer.Corporation.Actions.Length];
            Transform actionPanel = transform.Find("ActionPanel").Find("PlayerActions");
            if (actionPanel != null)
            {
                for (int i = 0; i < humanPlayer.Corporation.Actions.Length; i++)
                {
                    UIAction action = actionPanel.Find($"Action{i + 1}").GetComponent<UIAction>();
                    HumanActions[i] = action;
                    action.GameAction = humanPlayer.Corporation.Actions[i];
                    action.TextBox.text = action.GameAction.Name;
                    action.Button.onClick.AddListener(delegate { ActionClicked(action); });
                }
            }
            BotActions = new UIAction[botPlayer.Corporation.Actions.Length];
            actionPanel = transform.Find("ActionPanel").Find("BotActions");
            if (actionPanel != null)
            {
                for (int i = 0; i < botPlayer.Corporation.Actions.Length; i++)
                {
                    UIAction action = actionPanel.Find($"Action{i + 1}").GetComponent<UIAction>();
                    BotActions[i] = action;
                    action.GameAction = botPlayer.Corporation.Actions[i];
                    action.TextBox.text = action.GameAction.Name;
                    //TODO: no button for bot
                    action.Button.interactable = false;
                }
            }
        }

        public void BuildInfo(Player player)
        {
            Transform playerPanel = transform.Find("PlayerPanel");
            if (playerPanel != null)
            {
                TextMeshProUGUI discStockText = playerPanel.Find("DiscStock").Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI expansionPointsText = playerPanel.Find("ExpansionPoints").Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI marketPointsText = playerPanel.Find("MarketPoints").Find("Text").GetComponent<TextMeshProUGUI>();

                discStockText.SetText($"Discs: {player.DiscStock}");
                expansionPointsText.SetText($"EP: {player.ExpansionPoints}");
                marketPointsText.SetText($"MP: {player.MarketPoints}");

            }
        }

        public void SetElementLock(UIElements element, bool isLocked)
        {
            switch (element)
            {
                case UIElements.PlayerActions:
                    Transform playerActions = transform.Find("ActionPanel").Find("PlayerActions");
                    foreach(Transform child in playerActions.transform)
                    {
                        Button button = child.GetComponentInChildren<Button>();
                        button.interactable = !isLocked;
                    }
                    break;

                default:
                    break;
            }
        }

        public void SetActionLock(int action)
        {
            foreach(UIAction uiAction in HumanActions)
            {
                if (uiAction.GameAction.DieFace == action)
                {
                    uiAction.Button.interactable = true;
                }
                else
                    uiAction.Button.interactable = false;
            }
        }

        private void ActionClicked(UIAction action)
        {
            uiEvents.SelectAction(action);
        }
    }
}