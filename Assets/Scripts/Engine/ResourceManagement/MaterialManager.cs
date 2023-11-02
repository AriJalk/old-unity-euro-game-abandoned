using System.Collections.Generic;
using UnityEngine;

namespace EDBG.Engine.ResourceManagement
{
    /// <summary>
    /// Loads materials
    /// </summary>
    public class MaterialManager : MonoBehaviour
    {

        private Dictionary<string, Material> materialPool;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Initialize()
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