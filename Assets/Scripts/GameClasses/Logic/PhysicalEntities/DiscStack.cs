using System.Collections.Generic;
using UnityEngine;

public class DiscStack
{
    private List<Disc> discsList;

    public int Count
    {
        get { return discsList.Count; }
    }

    public DiscStack()
    {
        discsList = new List<Disc>();
    }

    public DiscStack(DiscStack otherStack)
    {
        discsList = new List<Disc>(otherStack.discsList);
    }

    public void PushDisc(Disc disc)
    {
        discsList.Add(disc);
    }

    public Disc PopDisc()
    {
        if (discsList.Count == 0)
        {
            Debug.Log("Discs Stack is Empty");
            return null;
        }

        int lastIndex = discsList.Count - 1;
        Disc poppedDisc = discsList[lastIndex];
        discsList.RemoveAt(lastIndex);
        return poppedDisc;
    }

    public Disc GetDiscByIndex(int index)
    {
        if (index < 0 || index >= discsList.Count)
        {
            Debug.Log("Invalid disc index");
            return null;
        }

        return discsList[index];
    }

    public Disc PeekTopDisc()
    {
        if (discsList.Count == 0)
        {
            Debug.Log("Discs Stack is Empty");
            return null;
        }

        return discsList[discsList.Count - 1];
    }
}