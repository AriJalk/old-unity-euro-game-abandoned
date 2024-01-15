//TODO: Interface from class
using UnityEngine;
using UnityEngine.SceneManagement;

using EDBG.Engine.ResourceManagement;
using EDBG.Engine.Visual;
using EDBG.Engine.InputManagement;
using EDBG.Engine.Animation;

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

        public InputHandler InputHandler { get; private set; }
        public InputEvents InputEvents { get; private set; }
        public SpriteManager SpriteManager { get; private set; }
        public PlatformManager PlatformManager { get; private set; }
        public MaterialManager MaterialManager { get; private set; }
        public MapRenderer MapRenderer { get; private set; }
        public ObjectsRenderer ObjectsRenderer { get; private set; }
        public ColorManager ColorManager { get; private set; }

        //Mono Objects
        public PrefabManager PrefabManager;
        public ScreenManager ScreenManager;


        void Awake()
        {
            
            InputEvents = new InputEvents();
            InputHandler = new InputHandler();
            MaterialManager = new MaterialManager();
            ColorManager = new ColorManager();
            MapRenderer = new MapRenderer();
            ObjectsRenderer = new ObjectsRenderer();
            PlatformManager = new PlatformManager();
            SpriteManager = new SpriteManager();

            ScreenManager.Initialize();
            PrefabManager.Initialize();
            Random.InitState((int)System.DateTime.Now.Ticks);
        }

        void Update()
        {
            InputHandler.Listen();
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
