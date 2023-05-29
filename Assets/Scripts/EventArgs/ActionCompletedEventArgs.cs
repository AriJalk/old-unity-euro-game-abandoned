using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCompletedEventArgs : EventArgs
{
    public List<Names.EntityNames> ItemsToUpdate;

    public ActionCompletedEventArgs(List<Names.EntityNames> items)
    {
        ItemsToUpdate = items;
    }
}
