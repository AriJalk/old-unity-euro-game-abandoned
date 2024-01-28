using UnityEngine;
using UnityEngine.UI;

using EDBG.Engine.Core;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private EngineManagerScpritableObject engineManager;
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

    //TODO: work with new engine class
    void LoadGameTest()
    {
        engineManager.LoadScene("GameTestScene");
    }
}
