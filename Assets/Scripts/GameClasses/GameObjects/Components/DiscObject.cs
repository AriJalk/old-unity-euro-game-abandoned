using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscObject : MonoBehaviour
{

    public const float DISC_DIAMETER = 0.1f;
    public const float DISC_HEIGHT = 0.03f;

    public Disc discData
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initiazlize()
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
