using EDBG.Engine.ResourceManagement;
using EDBG.GameLogic.Components;
using System.Collections.Generic;
using UnityEngine;

public class DiceTrayObject : MonoBehaviour
{
    const int DicePerLine = 3;
    DiceTray diceTray;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {

    }

    //TODO: more than 5 dice
    public void SetDice(DiceTray diceTray, PrefabManager prefabManager)
    {
        foreach(DieObject die in transform.GetComponentsInChildren<DieObject>())
        {
            prefabManager.ReturnPoolObject(die);
        }
        this.diceTray = diceTray;
        for (int i = 0; i < diceTray.Dice.Count; i++)
        {
            DieObject dieObject = prefabManager.RetrievePoolObject<DieObject>();
            if (dieObject == null)
            {
                Debug.Log("NULL");
                return;
            }
            dieObject.Die = diceTray.Dice[i];
            dieObject.transform.SetParent(transform);
            //Arrange in rows
            float xMod = 0f;
            float zMod = 0f;
            if(i < DicePerLine)
            {
                xMod = -2f + i * 2f;
                zMod = 0f;
            }
            else
            {
                xMod = -1f + (i - DicePerLine) * 2f;
                zMod = -1.5f;
            }
            dieObject.transform.localPosition = new Vector3(xMod, 0.5f, zMod);


        }
    }
}
