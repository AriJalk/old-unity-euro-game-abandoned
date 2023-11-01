using EDBG.GameLogic.Rules;
using System;
using System.Collections.Generic;

namespace EDBG.GameLogic.Core
{
    public class ActionsManager
    {
        private Dictionary<string, GameAction> actions;

        public ActionsManager()
        {
            actions = new Dictionary<string, GameAction>();
        }

        public void RegisterAction(string actionName, GameAction action)
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

        public GameAction GetAction(string actionName)
        {
            if (actions.TryGetValue(actionName, out GameAction action))
            {
                return action;
            }

            throw new KeyNotFoundException("Action not found: " + actionName);
        }
    }
}
