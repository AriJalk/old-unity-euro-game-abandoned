using EDBG.Engine.Core;
using UnityEngine;

public static class EngineUtilities
{
    public static void CleanObject(Transform transform)
    {
        foreach(Transform child in transform)
        {
            CleanObject(child);
            GameEngineManager.Instance.PrefabManager.ReturnPoolObject(child);
        }
    }
}