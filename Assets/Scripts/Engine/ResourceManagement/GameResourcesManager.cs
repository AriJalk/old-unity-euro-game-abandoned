using UnityEngine;

namespace EDBG.Engine.ResourceManagement
{
    public class GameResourcesManager
    {
        public MaterialManager MaterialManager { get; private set; }
        public PrefabManager PrefabManager { get; private set; }
        public SpriteManager SpriteManager { get; private set; }

        public GameResourcesManager() 
        {
            MaterialManager = new MaterialManager();
            Transform unactiveObjects = new GameObject("UnactiveObjects").transform;
            PrefabManager = new PrefabManager(unactiveObjects);
            SpriteManager = new SpriteManager();
        }
    }
}