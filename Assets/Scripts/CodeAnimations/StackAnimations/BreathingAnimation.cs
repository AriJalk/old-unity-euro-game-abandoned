using EDBG.Director;
using UnityEngine;

public class BreathingAnimation : CodeAnimationBase
{
    [Range(0.1f, 10f)]
    public float Frequency = 1.5f;
    public float Amplitude = 0.25f;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;
        float sinValue = Mathf.Sin(elapsedTime * Frequency);

        // Scale based on sinValue, but clamp to prevent reduction
        float newScale = Mathf.Clamp(1f + Amplitude * sinValue, 1f, Mathf.Infinity);

        // Check if the scale has reached 1, then reset startTime
        if (newScale == 1f)
        {
            startTime = Time.time;
        }

        // Apply the new scale
        transform.localScale = new Vector3(newScale, newScale, newScale);

    }


    private void OnDestroy()
    {
        transform.localScale = Vector3.one;
    }
    public override void PlayAnimation()
    {
        // Enable the script
        enabled = true;

        // Log for debugging
        Debug.Log("Animation started");
    }

    public override void StopAnimation()
    {
        transform.localScale = Vector3.one;
        base.StopAnimation();
        Destroy(this);
    }
}
