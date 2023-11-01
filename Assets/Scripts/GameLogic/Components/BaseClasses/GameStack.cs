using System.Collections.Generic;
using UnityEngine;

namespace EDBG.GameLogic.Components
{

    public class GameStack<T> : IGameComponent where T : IGameComponent
    {
        private List<T> itemsList;

        public int Count
        {
            get { return itemsList.Count; }
        }

        public GameStack()
        {
            itemsList = new List<T>();
        }

        protected GameStack(GameStack<T> otherStack)
        {
            int count = otherStack.Count;
            itemsList = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                itemsList.Add(otherStack.GetItemByIndex(i));
            }
        }

        //TODO: Check if shallow or deep copy
        public GameStack(List<T> list)
        {
            itemsList = new List<T>(list);
        }

        public void PushItem(T item)
        {
            itemsList.Add(item);
        }

        public T PopItem()
        {
            if (itemsList.Count == 0)
            {
                Debug.Log("Stack is Empty");
                return null;
            }

            int lastIndex = itemsList.Count - 1;
            T poppedItem = itemsList[lastIndex];
            itemsList.RemoveAt(lastIndex);
            return poppedItem;
        }

        public T GetItemByIndex(int index)
        {
            if (index < 0 || index >= itemsList.Count)
            {
                Debug.Log("Invalid item index");
                return null;
            }

            return itemsList[index];
        }

        public T PeekTopItem()
        {
            if (itemsList.Count == 0)
            {
                Debug.Log("Stack is Empty");
                return null;
            }

            return itemsList[itemsList.Count - 1];
        }

        public void RemoveItem(T item)
        {
            itemsList.Remove(item);
        }

        public override object Clone()
        {
            GameStack<T> clone = new GameStack<T>();
            foreach (T item in itemsList)
            {
                clone.itemsList.Add(item.Clone() as T);
            }
            return new GameStack<T>(this);
        }
    }

}
