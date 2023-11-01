using System;
using System.Collections.Generic;
using EDBG.GameLogic.Rules;

namespace EDBG.Engine.Utilities
{
    public class ActionCompletedEventArgs : EventArgs
    {
        public List<EntityNames> ItemsToUpdate;

        public ActionCompletedEventArgs(List<EntityNames> items)
        {
            ItemsToUpdate = items;
        }
    }

}
