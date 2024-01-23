using EDBG.Director;
using UnityEngine;

/// <summary>
/// Disc dropping into position from above
/// </summary>
public class PlaceDiscAnimation : CodeAnimationBase
{
    public float DropHeight = 1.0f;
    public float Duration = 0.3f;

    private float elapsedTime;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
        transform.position = targetPosition + Vector3.up * DropHeight;
        initialPosition = transform.position;
        NewDirector.Instance.AddAnimation(this);
    }

    void Update()
    {
        // Calculate the normalized time (0 to 1) based on the elapsed time and duration
        float normalizedTime = elapsedTime / Duration;

        // Clamp normalizedTime to ensure it stays within [0, 1]
        normalizedTime = Mathf.Clamp01(normalizedTime);

        transform.position = Vector3.Lerp(initialPosition, targetPosition, normalizedTime);

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
        transform.position = targetPosition;
        Destroy(this);
    }
}
