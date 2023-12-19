using EDBG.Engine.Core;
using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.Rules;
using UnityEngine;

namespace EDBG.Engine.Visual
{
    public class ColorManager
    {
        private static ColorManager _instance;

        public static ColorManager Instance
        {
            get 
            {
                return _instance; 
            }
        }


        MaterialManager materialManager;

        public ColorManager()
        {
            materialManager = GameEngineManager.Instance.MaterialManager;
            _instance = this;
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