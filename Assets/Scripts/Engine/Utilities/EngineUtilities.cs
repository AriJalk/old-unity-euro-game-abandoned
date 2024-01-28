using EDBG.Engine.Core;
using EDBG.GameLogic.Core;
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
}