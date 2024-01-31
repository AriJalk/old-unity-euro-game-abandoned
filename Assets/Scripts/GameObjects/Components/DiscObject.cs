using UnityEngine;

using EDBG.GameLogic.Components;

public class DiscObject : MonoBehaviour
{
    public Transform Model;

    public const float DISC_DIAMETER = 0.1f;
    public const float DISC_HEIGHT = 0.03f;
    public const float DISC_SCALE = 2.5f;
    public const float FILLER_FACTOR = 3f;


    public Disc DiscData
    {
        get; set;
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


    public void ApplyMaterial(Material material)
    {

        // Get the Renderer component of the modelObject
        Renderer modelRenderer = Model.GetComponent<Renderer>();

        // Assign the new material to the modelRenderer
        modelRenderer.material = material;
    }

}
