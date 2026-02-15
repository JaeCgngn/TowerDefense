using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    [Header("Idle Heartbeat")]
    public float idleAmplitude = 0.05f;
    public float idleSpeed = 2f;

    [Header("Triggered Beat")]
    public float triggeredScale = 1.3f;
    public float triggeredDuration = 0.2f;

    private Vector3 originalScale;
    private float triggerTimer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        FollowRoute.OnRouteFinished += OnRouteFinished;
    }

    void OnDestroy()
    {
        FollowRoute.OnRouteFinished -= OnRouteFinished;
    }

    private void OnRouteFinished()
    {
        triggerTimer = triggeredDuration * 2f;
    }

    void Update()
    {
        //Idle Pulse
        float idlePulse = Mathf.Sin(Time.time * idleSpeed) * idleAmplitude;

        //Triggered Beat 
        float triggeredPulse = 0f;
        if (triggerTimer > 0f)
        {
            triggerTimer -= Time.deltaTime;
            float t = Mathf.Clamp01(triggerTimer / (triggeredDuration * 2f));
            // Smooth scale
            triggeredPulse = (t > 0.5f)
                ? Mathf.Lerp(triggeredScale - 1f, 0f, (1f - t) * 2f)  // scaling down
                : Mathf.Lerp(triggeredScale - 1f, 0f, t * 2f);        // scaling up
        }

        // Apply combined scale
        float finalScale = 1f + idlePulse + triggeredPulse;
        transform.localScale = originalScale * finalScale;
    }
}
