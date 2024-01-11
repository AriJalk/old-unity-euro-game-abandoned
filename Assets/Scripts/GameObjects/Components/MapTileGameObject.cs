using UnityEngine;
using EDBG.GameLogic.MapSystem;

public class MapTileGameObject : MonoBehaviour
{

    public const float TILE_LENGTH = 0.5f;
    public const float TILE_SPACING = 0.01f;
    public const float TILE_HEIGHT = 0.05f;

    public AnimatedObject Stack;

    public MapTile TileData
    {
        get;
        set;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ApplyMaterial(Material material)
    {
        // Find the child GameObject with the model
        GameObject modelObject = transform.Find("SquareTileModel").gameObject;

        // Get the Renderer component of the modelObject
        Renderer modelRenderer = modelObject.GetComponent<Renderer>();

        // Assign the new material to the modelRenderer
        modelRenderer.material = material;
    }

}