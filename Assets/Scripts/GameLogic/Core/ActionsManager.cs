using System;
using System.Collections;
using System.Collections.Generic;

public class ActionsManager
{
    private Dictionary<string, GameActionBase> actions;

    public ActionsManager()
    {
        actions = new Dictionary<string, GameActionBase>();
    }

    public void RegisterAction(string actionName, GameActionBase action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action), "Action cannot be null");
        }

        if (actions.ContainsKey(actionName))
        {
            throw new ArgumentException("Action name already exists: " + actionName, nameof(actionName));
        }

        actions.Add(actionName, action);
    }

    public GameActionBase GetAction(string actionName)
    {
        if (actions.TryGetValue(actionName, out GameActionBase action))
        {
            return  action;
        }

        throw new KeyNotFoundException("Action not found: " + actionName);
    }
}