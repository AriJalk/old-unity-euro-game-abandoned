using EDBG.Director;
using EDBG.Engine.ResourceManagement;
using System.Resources;
using UnityEngine;

namespace EDBG.Engine.Visual
{
    public class VisualManager
    {
        public MapRenderer MapRenderer { get; private set; }
        public ObjectsRenderer ObjectsRenderer { get; private set; }
        public ScreenManager ScreenManager { get; private set; }
        public ColorManager ColorManager { get; private set; }


        public GameResourcesManager ResourcesManager { get; private set; }
        public AnimationManager AnimationManager { get; private set; }
        public MapHolder MapHolder { get; private set; }

        public VisualManager(GameResourcesManager resourceManager, MapHolder mapHolder, Transform parent)
        {
            ResourcesManager = resourceManager;
            MapHolder = mapHolder;

            MapRenderer = new MapRenderer(this);
            ObjectsRenderer = new ObjectsRenderer(this);

            ColorManager = new ColorManager(this);

            ScreenManager = new GameObject("Screen Manager").AddComponent<ScreenManager>();
            ScreenManager.transform.SetParent(parent);

            AnimationManager = new AnimationManager();
        }
    }
}