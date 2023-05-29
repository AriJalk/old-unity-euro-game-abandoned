using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager
{
    private Dictionary<string, IAction> actions;

    public ActionsManager()
    {
        actions = new Dictionary<string, IAction>();
    }

    public void RegisterAction(string actionName, IAction action)
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

    public IAction GetAction(string actionName)
    {
        if (actions.TryGetValue(actionName, out IAction action))
        {
            return  action;
        }

        throw new KeyNotFoundException("Action not found: " + actionName);
    }
}