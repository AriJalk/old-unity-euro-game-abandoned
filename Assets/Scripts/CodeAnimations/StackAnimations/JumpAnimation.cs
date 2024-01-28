using EDBG.Director;
using EDBG.Engine.Core;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Core;
using UnityEngine;

public class JumpAnimation : CodeAnimationBase
{
    public MapTileGameObject TargetTile;
    public float JumpHeight = 1.0f;
    public float Duration = 1.0f;

    private Vector3 initialPosition;
    private float elapsedTime;

    void Start()
    {
        initialPosition = transform.position;
        //Swap stacks
        Transform stack = TargetTile.StackContainer.Find("Stack");
        
        stack.SetParent(transform.parent);
        stack.localPosition = Vector3.zero;
        
        foreach(DiscObject disc in stack.GetComponentsInChildren<DiscObject>())
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().EngineManager.ResourcesManager.PrefabManager.ReturnPoolObject(disc);
        }
        transform.SetParent(TargetTile.StackContainer);
        
        NewDirector.Instance.AddAnimation(this);
    }

    void Update()
    {
        // Calculate the normalized time (0 to 1) based on the elapsed time and duration
        float normalizedTime = elapsedTime / Duration;

        // Clamp normalizedTime to ensure it stays within [0, 1]
        normalizedTime = Mathf.Clamp01(normalizedTime);

        // Use a curve for the jump animation
        float jumpCurveValue = Mathf.Sin(normalizedTime * Mathf.PI);

        Debug.Log("Vector - " + Vector3.Lerp(initialPosition, TargetTile.StackContainer.position, normalizedTime).ToString());

        // Interpolate between initial and target positions
        transform.position = Vector3.Lerp(initialPosition, TargetTile.StackContainer.position, normalizedTime) + Vector3.up * jumpCurveValue * JumpHeight;

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // If the animation is complete, perform any necessary cleanup
        if (normalizedTime >= 1.0f)
        {
            // Animation complete, do cleanup or handle events
            enabled = false;
            NewDirector.Instance.OnAnimationEnd(this);
            Destroy(this);
        }
    }

    public override void PlayAnimation()
    {
        // Initialize the animation by resetting the elapsed time
        elapsedTime = 0.0f;
        // Enable the script
        enabled = true;

        // Log for debugging
        Debug.Log("Animation started");
    }

    public override void StopAnimation()
    {
        transform.position = TargetTile.StackContainer.position;
        Destroy(this);
    }
}
