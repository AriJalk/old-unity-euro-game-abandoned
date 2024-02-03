using EDBG.GameLogic.Core;
using EDBG.UserInterface;
using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum UICommands
{
    Confirm,
    Undo,
}
public class OverlayUI : MonoBehaviour
{
    public UnityEvent<UICommands> ButtonEvent;

    [SerializeField]
    private TextMeshProUGUI StatusText;
    [SerializeField]
    private TextMeshProUGUI PStock;
    [SerializeField]
    private TextMeshProUGUI HStock;

    [SerializeField]
    private Button confirmButton;
    [SerializeField]
    private Button undoButton;


    private void OnDestroy()
    {
        ButtonEvent.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
    }

    private void OnButtonClick(Button button)
    {
        if (button == confirmButton)
        {
            ButtonEvent?.Invoke(UICommands.Confirm);
        }
        else if (button == undoButton)
        {
            ButtonEvent.Invoke(UICommands.Undo);
        }
    }

    private void UpdateStatusText(string text)
    {
        StatusText.text = text;
        Debug.Log(text);
    }

    private void UpdateStock(string text)
    {
        if (text.StartsWith("1"))
            PStock.text = text;
        if (text.StartsWith("2"))
            HStock.text = text;

    }

    public void Initialize(GameManager manager)
    {
        manager.StatusMessageEvent.AddListener(UpdateStatusText);
        manager.StockChangedEvent.AddListener(UpdateStock);
        confirmButton.onClick.AddListener(delegate { OnButtonClick(confirmButton); });
        undoButton.onClick.AddListener(delegate { OnButtonClick(undoButton); });
        ButtonEvent = new UnityEvent<UICommands>();
    }

}

