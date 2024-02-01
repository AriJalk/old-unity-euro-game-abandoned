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
    public void Initialize(GameManager manager)
    {
        manager.GameMessageEvent.AddListener(UpdateStatusText);
        confirmButton.onClick.AddListener(delegate { OnButtonClick(confirmButton); });
        undoButton.onClick.AddListener(delegate { OnButtonClick(undoButton); });
        ButtonEvent = new UnityEvent<UICommands>();
    }

}

