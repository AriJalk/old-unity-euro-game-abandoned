using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceObject : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    public Button UpdateButton;
    public Button ActionButton;
    private GameObject canvas;

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

    public void Initialize()
    {
        canvas = transform.Find("Canvas").gameObject;
        textBox = canvas.transform.Find("TextBox").GetComponent<TextMeshProUGUI>();
        UpdateButton = canvas.transform.Find("UpdateButton").GetComponent<Button>();
        ActionButton = canvas.transform.Find("ActionButton").GetComponent<Button>();
    }

    public Button GetButton()
    {
        return UpdateButton;
    }
}
