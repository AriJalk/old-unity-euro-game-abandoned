using EDBG.Engine.Core;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.Rules;
using UnityEngine;

namespace EDBG.Engine.Visual
{
    public class ColorManager
    {
        MaterialManager materialManager;

        public ColorManager()
        {
            materialManager = GameEngineManager.Instance.MaterialManager;
        }

        public Material GetDiscMaterial(DiscColors color)
        {
            return materialManager.GetMaterial($"DiscColors/{color}WoodMaterial");
        }

        public Material GetTileMaterial(TileColors color)
        {
            return materialManager.GetMaterial($"{color}Material");
        }
    }
}