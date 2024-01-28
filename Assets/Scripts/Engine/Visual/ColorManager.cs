using EDBG.Engine.Core;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.Rules;
using UnityEngine;

namespace EDBG.Engine.Visual
{
    //TODO: combine with MaterialManager
    public class ColorManager
    {
        MaterialManager materialManager;

        public ColorManager(MaterialManager materialManager)
        {
            this.materialManager = materialManager;
        }

        public Material GetDiscMaterial(PlayerColors color)
        {
            return materialManager.GetMaterial($"DiscColors/{color}WoodMaterial");
        }

        public Material GetTileMaterial(TileColors color)
        {
            //return materialManager.GetMaterial($"{color}Material");
            return materialManager.GetMaterial($"TileColors/{color}TileMaterial");
        }

        public Material GetMaterial(string name)
        {
            return materialManager.GetMaterial($"{name}Material");
        }
    }
}