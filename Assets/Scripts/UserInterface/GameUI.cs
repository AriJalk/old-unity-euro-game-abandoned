using UnityEngine;
using UnityEngine.UI;

using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

using TMPro;

namespace EDBG.UserInterface
{
}
public class GameUI : MonoBehaviour
{

    void Start()
    {

    }

    public void Initialize(Player humanPlayer, Player botPlayer)
    {
        BuildActions(humanPlayer, botPlayer);
        BuildInfo(humanPlayer);
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

    public void BuildActions(Player humanPlayer, Player botPlayer)
    {
        Transform actionPanel = transform.Find("ActionPanel").Find("PlayerActions");
        if (actionPanel != null)
        {
            for (int i = 0; i < 6; i++)
            {
                Transform action = actionPanel.Find($"Action{i + 1}");
                Transform button = action.Find("Button");
                TextMeshProUGUI text = button.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                text.text = humanPlayer.Corporation.Actions[i].Name;
            }
        }
        actionPanel = transform.Find("ActionPanel").Find("BotActions");
        if (actionPanel != null)
        {
            for (int i = 0; i < 3; i++)
            {
                Transform action = actionPanel.Find($"Action{i + 1}");
                TextMeshProUGUI text = action.Find("Text").GetComponent<TextMeshProUGUI>();
                text.text = botPlayer.Corporation.Actions[i].Name;
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

            discStockText.SetText($"Discs: {player.DiscStock.ToString()}");
            expansionPointsText.SetText($"EP: {player.ExpansionPoints.ToString()}");
            marketPointsText.SetText($"MP: {player.MarketPoints.ToString()}");

        }
    }
}

