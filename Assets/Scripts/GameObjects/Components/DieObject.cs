using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieObject : MonoBehaviour
{

    Die die;

    public int DieResult
    {
        get
        {
            if (die != null)
                return die.Result;
            return 0;
        }
    }


    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        die = new Die();
        die.Roll();
        SetObjectRotation(die.Result);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetObjectRotation(int topFace)
    {
        switch (topFace)
        {
            case 1:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case 2:
                transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                break;
            case 3:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
            case 4:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case 5:
                transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                break;
            case 6:
                transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                break;
            default:
                break;
        }
    }

    public void SetTopFace(int face)
    {
        if (face > 0 && face <= 6)
        {
            die.ChangeValue(face);
            SetObjectRotation((die.Result));
        }
    }
}
