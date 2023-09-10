using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        Transform buttonCanvas = transform.Find("ButtonsCanvas");
        Button gameTestButton = buttonCanvas.Find("GameTestButton").GetComponent<Button>();
        Button uiTestButton = buttonCanvas.Find("UITestButton").GetComponent<Button>();

        gameTestButton.onClick.AddListener(LoadGameTest);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadGameTest()
    {
        GameEngineManager.Instance.LoadScene("GameTestScene");
    }
}
