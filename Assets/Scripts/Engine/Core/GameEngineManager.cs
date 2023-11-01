//TODO: Interface from class
using UnityEngine;
using UnityEngine.SceneManagement;

using EDBG.Engine.ResourceManagement;
using EDBG.Engine.Visual;
using EDBG.Engine.InputManagement;

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

        public PoolManager PoolManager;
        public MapRenderer MapRenderer;
        public ObjectsRenderer DiscRenderer;
        public MaterialManager MaterialManager;
        public InputHandler InputHandler;
        public PlatformManager PlatformManager;
        public ScreenManager ScreenManager;
        public SpriteManager SpriteManager;
        public InputEvents InputEvents;


        void Awake()
        {
            InitizalizeScripts();

        }

        void InitizalizeScripts()
        {
            InputEvents = new InputEvents();
            InputHandler.Initialize();
            PoolManager.Initialize();
            MaterialManager.Initialize();
            MapRenderer.Initialize();
            DiscRenderer.Initialize();
            PlatformManager.Initialize();
            ScreenManager.Initialize();
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
