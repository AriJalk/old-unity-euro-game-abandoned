using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

namespace ResourcePool
{
    public class ObjectPool<T> where T : Component
    {
        //Fields
        private Queue<T> queue;

        public ObjectPool()
        {
            queue = new Queue<T>();
        }

        public T RetrieveQueueObject()
        {
            if (queue == null)
                return null;
            if (queue.Count == 0)
                return null;
            return queue.Dequeue();
        }


        public void AddQueueObject(T newObj)
        {
            queue.Enqueue(newObj);
        }
    }

}
