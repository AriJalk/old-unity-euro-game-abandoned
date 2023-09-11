using EDBG.Rules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    void Start()
    {
        
    }

    public void Initialize()
    {

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
                case EDBG.Rules.TokenColors.Red:
                    color = Color.red;
                    break;
                case EDBG.Rules.TokenColors.Green:
                    color = Color.green;
                    break;
                case EDBG.Rules.TokenColors.Blue:
                    color = Color.blue;
                    break;
                default:
                    color = Color.white;
                    break;
            }
            tokenImage.GetComponent<RawImage>().color = color;
            tokenImage.GetComponent<RawImage>().texture = (Texture)Resources.Load("Images/GreenToken");
            tokenImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tokenSize.x);
            tokenImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tokenSize.y);

            Destroy(emptyObject);


        }
    }
}
