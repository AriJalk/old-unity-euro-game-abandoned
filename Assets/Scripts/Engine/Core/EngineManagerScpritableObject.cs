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
    public PrefabManager PrefabManager { get; private set; }
    public SpriteManager SpriteManager { get; private set; }
    public PlatformManager PlatformManager { get; private set; }
    public MaterialManager MaterialManager { get; private set; }
    public MapRenderer MapRenderer { get; private set; }
    public ObjectsRenderer ObjectsRenderer { get; private set; }
    public ColorManager ColorManager { get; private set; }



    public void InitializeScene()
    {
        engineComponents = new GameObject("Engine Components").transform;

        PrefabManager = new PrefabManager(new GameObject("Unactive Objects").transform);
        SpriteManager = new SpriteManager();
        PlatformManager = new PlatformManager();
        MaterialManager = new MaterialManager();
        ColorManager = new ColorManager(MaterialManager);
        MapRenderer = new MapRenderer();
        ObjectsRenderer = new ObjectsRenderer(ColorManager);

        ScreenManager = new GameObject("Screen Manager").AddComponent<ScreenManager>();
        ScreenManager.transform.SetParent(engineComponents);

        InputManager = new GameObject("Input Manager").AddComponent<InputManager>();
        InputManager.transform.SetParent(engineComponents);

        Random.InitState((int)System.DateTime.Now.Ticks);

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // You can do other things here, or yield return null to wait for the next frame
            yield return null;
        }

        InitializeScene();
    }
}
