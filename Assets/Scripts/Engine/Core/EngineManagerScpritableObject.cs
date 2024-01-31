using EDBG.Director;
using EDBG.Engine.Core;
using EDBG.Engine.InputManagement;
using EDBG.Engine.ResourceManagement;
using EDBG.Engine.Visual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EngineManagerScriptableObject", menuName = "Engine Manager Scriptable Object")]
public class EngineManagerScpritableObject : ScriptableObject
{
    private Transform engineComponents;

    //Mono Managers
    public InputManager InputManager { get; private set; }
    public ScreenManager ScreenManager { get; private set; }

    //Regular Manager
    public GameResourcesManager ResourcesManager { get; private set; }
    public PlatformManager PlatformManager { get; private set; }
    public VisualManager VisualManager { get; private set; }


    public void InitializeScene(MapHolder mapHolder)
    {
        engineComponents = new GameObject("Engine Components").transform;

        ResourcesManager = new GameResourcesManager();
        PlatformManager = new PlatformManager();
        VisualManager = new VisualManager(ResourcesManager, mapHolder,engineComponents);




        InputManager = new GameObject("Input Manager").AddComponent<InputManager>();
        InputManager.transform.SetParent(engineComponents);

        ScreenManager = new GameObject("Screen Manager").AddComponent<ScreenManager>();
        ScreenManager.transform.SetParent(engineComponents);

        Random.InitState((int)System.DateTime.Now.Ticks);

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
