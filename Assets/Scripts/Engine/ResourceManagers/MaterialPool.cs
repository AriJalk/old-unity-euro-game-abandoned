using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPool : MonoBehaviour
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
            Material material = Resources.Load<Material>(materialPath);
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
