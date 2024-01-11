using UnityEngine;

using EDBG.GameLogic.Components;
using EDBG.Engine.Animation;

public class DiscObject : MonoBehaviour
{
    public const float DISC_DIAMETER = 0.1f;
    public const float DISC_HEIGHT = 0.03f;
    public const float DISC_SCALE = 2.5f;
    public const float FILLER_FACTOR = 3f;

    public AnimatedObject AnimatedObject { get; private set; }

    public Disc discData
    {
        get; set;
    }


    void Awake()
    {
        AnimatedObject = transform.Find("DiscModel").GetComponent<AnimatedObject>();
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
        // Find the child GameObject with the model
        GameObject modelObject = transform.Find("DiscModel").gameObject;

        // Get the Renderer component of the modelObject
        Renderer modelRenderer = modelObject.GetComponent<Renderer>();

        // Assign the new material to the modelRenderer
        modelRenderer.material = material;
    }

}
