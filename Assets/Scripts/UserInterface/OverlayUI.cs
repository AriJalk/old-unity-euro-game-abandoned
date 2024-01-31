using EDBG.GameLogic.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OverlayUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI StatusText;
    private void UpdateStatusText(string text)
    {
        StatusText.text = text;
        Debug.Log(text);
    }
    public void Initialize(GameManager manager)
    {
        manager.GameMessageEvent.AddListener(UpdateStatusText);
    }


}

