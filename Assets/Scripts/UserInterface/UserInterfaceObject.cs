using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//TODO: Link properly the engineManager through api
public class UserInterfaceObject : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    private Button updateButton;
    private Button undoButton;
    private Button extraButton;
    private Button actionButton;
    private Transform canvas;
    private Transform topPanel;
    private GameEngineManager gameEngineManager;
    private Image[] dice;
    private UILockManager lockManager = new UILockManager();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string value)
    {
        textBox.SetText(value);
    }

    public void Initialize(GameEngineManager manager)
    {
        gameEngineManager = manager;
        canvas = transform.Find("Canvas");
        topPanel = canvas.transform.Find("TopPanel");
        textBox = canvas.transform.Find("TextBox").GetComponent<TextMeshProUGUI>();
        updateButton = topPanel.transform.Find("UpdateButton").GetComponent<Button>();
        undoButton = topPanel.transform.Find("UndoButton").GetComponent<Button>();
        extraButton = topPanel.transform.Find("ExtraButton").GetComponent<Button>();
        actionButton = canvas.transform.Find("ActionButton").GetComponent<Button>();
        updateButton.onClick.AddListener(UpdateUI);
        undoButton.onClick.AddListener(UndoUI);
        extraButton.onClick.AddListener(ExtraTest);

        Transform panel = canvas.transform.Find("PlayerHandPanel");

        dice = new Image[3];

        for (int i = 0; i < 3; i++)
        {
            dice[i] = panel.transform.Find("Die" + (i + 1)).Find("Image").GetComponent<Image>();
        }

        for (int i = 0; i < dice.Length; i++)
        {
            dice[i].sprite = GetDieImage(i + 1);

        }

        Debug.Log(dice.Length);
    }

    private void UpdateUI()
    {
        if (!lockManager.IsElementLocked("UpdateButton"))
            gameEngineManager.TestMove();
    }

    private void UndoUI()
    {
        gameEngineManager.Undo();
    }

    public void AxisChanged(float horizonalInput, float verticalInput)
    {
        gameEngineManager.MoveCamera(horizonalInput, verticalInput);
    }

    public void MouseClicked(bool[] mouseButtons, Vector3 mousePos)
    {
        if (lockManager.IsElementLocked("Map"))
            return;
        if (mouseButtons[0] || mouseButtons[1])
        {
            gameEngineManager.SelectObject(mousePos, mouseButtons);
        }
    }

    public void ScreenChanged(ScreenOrientation orientation)
    {
        if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
        {

        }
        else if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
        {

        }
    }

    public void MouseScrolled(float deltaY)
    {
        gameEngineManager.ZoomCamera(deltaY);
    }

    private void ExtraTest()
    {
        lockManager.SetElementLock("UpdateButton", !lockManager.IsElementLocked("UpdateButton"));
        lockManager.SetElementLock("Map", !lockManager.IsElementLocked("Map"));
    }

    private Sprite GetDieImage(int face)
    {
        if (face < 1 || face > 6)
        {
            Debug.LogError("Face not valid");
            return null;
        }
        return Resources.Load<Sprite>("Images/DieFaces/" + (face + 1));
    }
}
