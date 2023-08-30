using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceObject : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    private Button UpdateButton;
    private Button UndoButton;
    private Button ActionButton;
    private GameObject canvas;
    private GameEngineManager gameEngineManager;
    private Image[] dice;

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
        canvas = transform.Find("Canvas").gameObject;
        textBox = canvas.transform.Find("TextBox").GetComponent<TextMeshProUGUI>();
        UpdateButton = canvas.transform.Find("UpdateButton").GetComponent<Button>();
        ActionButton = canvas.transform.Find("ActionButton").GetComponent<Button>();
        UndoButton = canvas.transform.Find("UndoButton").GetComponent<Button>();
        UpdateButton.onClick.AddListener(MoveTestUI);
        UndoButton.onClick.AddListener(UndoUI);

        Transform panel = canvas.transform.Find("PlayerHandPanel");

        dice = new Image[3];
        
        for(int i = 0; i < 3; i++)
        {
            dice[i]= panel.transform.Find("Die" + (i + 1)).Find("Image").GetComponent<Image>();
        }

        for (int i = 0; i < dice.Length; i++)
        {
            dice[i].sprite = Resources.Load<Sprite>("Images/DieFaces/" + (i+1));

        }

        Debug.Log(dice.Length);
    }

    private void MoveTestUI()
    {
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
}
