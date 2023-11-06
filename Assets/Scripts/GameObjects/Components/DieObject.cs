using UnityEngine;

using EDBG.GameLogic.Components;

public class DieObject : MonoBehaviour
{
    private Die _die;
    public Die Die 
    {
        get
        {
            return _die;
        }
        set
        {
            _die = value;
            SetObjectRotation();
        }
    }
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetObjectRotation()
    {
        switch (Die.Result)
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
            Die.ChangeValue(face);
            SetObjectRotation();
        }
    }
}
