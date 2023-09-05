using System.Collections.Generic;

public class UILockManager
{
    private readonly Dictionary<string, bool> elementLocks = new();

    // Use this method to lock/unlock a UI element
    public void SetElementLock(string elementName, bool isLocked)
    {
        if (elementLocks.ContainsKey(elementName))
        {
            elementLocks[elementName] = isLocked;
        }
        else
        {
            elementLocks.Add(elementName, isLocked);
        }
    }

    // Use this method to check if a UI element is locked
    public bool IsElementLocked(string elementName)
    {
        if (elementLocks.ContainsKey(elementName))
        {
            return elementLocks[elementName];
        }
        // If the element is not in the dictionary, consider it as unlocked by default.
        return false;
    }

    public void UnlockAll()
    {
        foreach (string elementName in elementLocks.Keys)
        { elementLocks[elementName] = false; }
    }

    
}