using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Engine.ResourceManagement
{
    /// <summary>
    /// Loads materials
    /// </summary>
    public class MaterialManager
    {

        private Dictionary<string, Material> materialPool;

        public MaterialManager()
        {
            materialPool = new Dictionary<string, Material>();
        }

        public Material GetMaterial(string materialPath)
        {
            if (materialPool.ContainsKey(materialPath))
            {
                return materialPool[materialPath];
            }
            else
            {
                Material material = Resources.Load<Material>($"Materials/{materialPath}");
                if (material == null)
                {
                    Debug.Log("Material not found at path: " + materialPath);
                }
                else
                {
                    materialPool[materialPath] = material;
                }
                return material;
            }
        }
    }

}