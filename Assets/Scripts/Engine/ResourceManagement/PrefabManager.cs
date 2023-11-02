using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace EDBG.Engine.ResourceManagement
{
    /// <summary>
    /// Class for prefab pooling
    /// </summary>
    public class PrefabManager : MonoBehaviour
    {
        private Dictionary<string, Queue<GameObject>> objectPools;
        private Dictionary<string, GameObject> prefabDict;
        private Transform unactiveObjects;
        private readonly int initialPoolSize = 100;

        public void Initialize()
        {
            objectPools = new Dictionary<string, Queue<GameObject>>();
            prefabDict = new Dictionary<string, GameObject>();
            unactiveObjects = transform.Find("UnactiveObjects");
        }

        void Awake()
        {
            
        }

        public void RegisterPrefab(string poolKey, GameObject prefab)
        {
            if (!prefabDict.ContainsKey(poolKey))
            {
                prefabDict.Add(poolKey, prefab);

                if (!objectPools.ContainsKey(poolKey))
                {
                    CreateObjectPool(poolKey, prefab);
                    for (int i = 0; i < initialPoolSize; i++)
                    {
                        GameObject obj = CreateInstance(poolKey);
                        obj.SetActive(false);
                        obj.transform.SetParent(unactiveObjects);
                        objectPools[poolKey].Enqueue(obj);
                    }
                }
            }
            else
            {
                prefabDict[poolKey] = prefab;
            }
        }

        private void CreateObjectPool(string poolKey, GameObject prefab)
        {
            objectPools.Add(poolKey, new Queue<GameObject>());
        }

        private GameObject CreateInstance(string poolKey)
        {
            GameObject prefab;
            if (prefabDict.TryGetValue(poolKey, out prefab))
            {
                return Instantiate(prefab);
            }
            else
            {
                Debug.Log("Prefab not registered for key: " + poolKey);
                return null;
            }
        }

        public GameObject RetrievePoolObject(string poolKey)
        {
            if (!objectPools.ContainsKey(poolKey))
            {
                CreateObjectPool(poolKey, prefabDict[poolKey]);
            }

            Queue<GameObject> pool = objectPools[poolKey];
            GameObject obj = pool.Count > 0 ? pool.Dequeue() : CreateInstance(poolKey);

            if (obj != null)
            {
                obj.SetActive(true);
            }
            return obj;
        }

        public void ReturnPoolObject(string poolKey, GameObject obj)
        {
            if (objectPools.ContainsKey(poolKey))
            {
                obj.SetActive(false);
                objectPools[poolKey].Enqueue(obj);
                obj.transform.SetParent(unactiveObjects);
            }
            else
            {
                Debug.Log("Pool not found for key: " + poolKey);
            }
        }
    }
}
