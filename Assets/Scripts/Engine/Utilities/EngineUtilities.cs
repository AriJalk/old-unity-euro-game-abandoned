using EDBG.Engine.Core;
using EDBG.GameLogic.Core;
using System.Collections.Generic;
using UnityEngine;

public static class EngineUtilities
{
    public static void CleanObject(Transform transform)
    {
        foreach(Transform child in transform)
        {
            CleanObject(child);
            GameObject.Find("Game Manager").GetComponent<GameManager>().EngineManager.ResourcesManager.PrefabManager.ReturnPoolObject(child);
        }
    }

    public static void SwapTransformParents(Transform transformA, Transform transformB)
    {
        Transform parentA = transformA.parent;
        transformA.SetParent(transformB.parent, true);
        transformB.SetParent(parentA, true);
    }
}