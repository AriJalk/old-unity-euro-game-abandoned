using System;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcePool
{
    /// <summary>
    /// Pool Manager for reusable objects
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
       

        private Dictionary<Type, object> pools;
        private Dictionary<Type, GameObject> prefabDict;
        private Transform unactiveObjects;
        private int initialPoolSize = 100;

       

        void Start()
        {
           
        }

        void Update()
        {
            // Your update logic here
        }

        void Awake()
        {

        }

        public void Initialize()
        {
            pools = new Dictionary<Type, object>();
            prefabDict = new Dictionary<Type, GameObject>();
            unactiveObjects = transform.Find("UnactiveObjects");
        }

        public void RegisterPrefab<T>(GameObject prefab) where T : Component
        {

            if (!prefabDict.ContainsKey(typeof(T)))
            {
                prefabDict.Add(typeof(T), prefab);

                if (!pools.ContainsKey(typeof(T)))
                {
                    SetQueue<T>();
                    ObjectPool<T> objectPool = (ObjectPool<T>)pools[typeof(T)];
                    for (int i = 0; i < initialPoolSize; i++)
                    {
                        T obj = CreateInstance<T>();
                        obj.gameObject.SetActive(false);
                        obj.gameObject.transform.SetParent(unactiveObjects);
                        objectPool.AddQueueObject(obj);
                    }
                }
            }
            else
            {
                prefabDict[typeof(T)] = prefab;
            }
        }

        private bool SetQueue<T>() where T : Component
        {
            if (pools.ContainsKey(typeof(T)))
                return false;

            ObjectPool<T> newPool = new ObjectPool<T>();
            pools.Add(typeof(T), newPool);

            return true;
        }

        private T CreateInstance<T>() where T : Component
        {
            GameObject prefab;
            if (prefabDict.TryGetValue(typeof(T), out prefab))
            {
                return Instantiate(prefab).GetComponent<T>();
            }
            else
            {
                Debug.Log("Prefab not registered for type: " + typeof(T));
                return null;
            }
        }

        public T RetrievePoolObject<T>() where T : Component, new()
        {
            if (!pools.ContainsKey(typeof(T)))
                SetQueue<T>();

            ObjectPool<T> objectPool = (ObjectPool<T>)pools[typeof(T)];
            T retrieval = objectPool.RetrieveQueueObject();

            if (retrieval == null)
            {
                retrieval = CreateInstance<T>();
            }

            if (retrieval == null)
                Debug.Log("Can't retrieve object");
            retrieval.gameObject.SetActive(true);
            return retrieval;
        }

        public void ReturnPoolObject<T>(T obj) where T : Component
        {
            if (pools.ContainsKey(typeof(T)))
            {
                ObjectPool<T> objectPool = (ObjectPool<T>)pools[typeof(T)];
                objectPool.AddQueueObject(obj);
                obj.gameObject.transform.SetParent(unactiveObjects);
                obj.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Pool not found for type: " + typeof(T));
            }
        }
    }
}
