using EDBG.Engine.Core;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.Rules;
using UnityEngine;

namespace EDBG.Engine.Visual
{
    //TODO: combine with MaterialManager
    public class ColorManager
    {
        VisualManager visualManager;

        public ColorManager(VisualManager visualManager)
        {
            this.visualManager = visualManager;
        }

        public Material GetDiscMaterial(PlayerColors color)
        {
            return visualManager.ResourcesManager.MaterialManager.GetMaterial($"DiscColors/{color}WoodMaterial");
        }

        public Material GetTileMaterial(TileColors color)
        {
            //return materialManager.GetMaterial($"{color}Material");
            return visualManager.ResourcesManager.MaterialManager.GetMaterial($"TileColors/{color}TileMaterial");
        }

        public Material GetMaterial(string name)
        {
            return visualManager.ResourcesManager.MaterialManager.GetMaterial($"{name}Material");
        }
    }
}