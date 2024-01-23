using EDBG.Director;
using UnityEngine;

public class JumpAnimation : CodeAnimationBase
{
    public Transform Target;
    public float JumpHeight = 1.0f;
    public float Duration = 1.0f;

    private Vector3 initialPosition;
    private float elapsedTime;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the normalized time (0 to 1) based on the elapsed time and duration
        float normalizedTime = elapsedTime / Duration;

        // Clamp normalizedTime to ensure it stays within [0, 1]
        normalizedTime = Mathf.Clamp01(normalizedTime);

        // Use a curve for the jump animation (you can customize this curve)
        float jumpCurveValue = Mathf.Sin(normalizedTime * Mathf.PI);

        Debug.Log("Vector - " + Vector3.Lerp(initialPosition, Target.position, normalizedTime).ToString());

        // Interpolate between initial and target positions
        transform.position = Vector3.Lerp(initialPosition, Target.position, normalizedTime) + Vector3.up * jumpCurveValue * JumpHeight;

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // If the animation is complete, perform any necessary cleanup
        if (normalizedTime >= 1.0f)
        {
            // Animation complete, do cleanup or handle events
            enabled = false;
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
}
