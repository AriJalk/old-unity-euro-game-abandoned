using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceObject : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    private Button UpdateButton;
    private Button ActionButton;
    private GameObject canvas;
    private GameEngineManager gameEngineManager;

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
        UpdateButton.onClick.AddListener(Something);
    }

    private void Something()
    {
        gameEngineManager.MoveDiscs(0, 0, 1, 1);
    }

    public void AxisChanged(float horizonalInput, float verticalInput)
    {
        gameEngineManager.MoveCamera(horizonalInput,verticalInput);
    }

    public void MouseClicked(bool[] mouseButtons, Vector3 mousePos)
    {
        if (mouseButtons[0])
        {
            gameEngineManager.SelectObject(mousePos);
        } 
    }
}
