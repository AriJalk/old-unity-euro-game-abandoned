using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStack<T> : GameComponent where T : class
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

    public GameStack(GameStack<T> otherStack)
    {
        itemsList = new List<T>(otherStack.itemsList);
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
}
