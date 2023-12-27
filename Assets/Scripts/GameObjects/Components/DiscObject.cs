using UnityEngine;

using EDBG.GameLogic.Components;

public class DiscObject : MonoBehaviour
{
    private static int PutDownAnimationHash = Animator.StringToHash("PutDiscTrigger");
    private Animator animator;
    public const float DISC_DIAMETER = 0.1f;
    public const float DISC_HEIGHT = 0.03f;

    public Disc discData
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("DiscModel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PutDiscDown()
    {
        if(animator != null)
        {

        }
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
