using System;
using System.Collections;
using System.Collections.Generic;
using EDBG.Rules;
using UnityEngine;

public class ActionCompletedEventArgs : EventArgs
{
    public List<EntityNames> ItemsToUpdate;

    public ActionCompletedEventArgs(List<EntityNames> items)
    {
        ItemsToUpdate = items;
    }
}
