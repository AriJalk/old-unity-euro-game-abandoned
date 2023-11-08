//TODO: Interface from class
using UnityEngine;
using UnityEngine.SceneManagement;

using EDBG.Engine.ResourceManagement;
using EDBG.Engine.Visual;
using EDBG.Engine.InputManagement;
using System.Runtime.InteropServices.WindowsRuntime;

namespace EDBG.Engine.Core
{
    /// <summary>
    /// Core engine manager, implemented as singleton to persist across scenes.
    /// Contains all unity technical managers
    /// </summary>
    /// TODO: remove all new GameObject
    public class GameEngineManager : MonoBehaviour
    {

        private static GameEngineManager instance;
        //TODO: fix persistance between scene?
        public static GameEngineManager Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameEngineManager>();

                    if (instance == null)
                    {
                        // If no instance exists, create a new GameObject with EngineManager and attach it to the "Engine" GameObject
                        GameObject managerObject = Instantiate(new GameObject("GameEngineManager"));
                        instance = managerObject.AddComponent<GameEngineManager>();
                    }

                    // Ensure the instance persists across scenes
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }

        public PrefabManager PrefabManager;
        public MapRenderer MapRenderer;
        public ObjectsRenderer ObjectsRenderer;
        public MaterialManager MaterialManager;
        public InputHandler InputHandler;
        public PlatformManager PlatformManager;
        public ScreenManager ScreenManager;
        public SpriteManager SpriteManager;
        public InputEvents InputEvents;
        //TODO: move to GameManager
        public ColorManager ColorManager {  get; private set; }

        void Awake()
        {
            InitizalizeScripts();

        }

        void InitizalizeScripts()
        {
            ColorManager = new ColorManager();
            InputEvents = new InputEvents();
            InputHandler.Initialize();
            PrefabManager.Initialize();
            MaterialManager.Initialize();
            MapRenderer.Initialize();
            ObjectsRenderer.Initialize();
            PlatformManager.Initialize();
            ScreenManager.Initialize();
            Random.InitState((int)System.DateTime.Now.Ticks);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
