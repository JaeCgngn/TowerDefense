using UnityEngine;

public class IdleFloatJuice : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Turret turret;

    [Header("Idle Float Settings")]
    [SerializeField] private float floatAmount = 0.5f;
    [SerializeField] private float floatSpeed = 1f;

    private Vector3 startPos;
    private bool isIdle = false;

    void Start()
    {
        if (!turret)
            turret = GetComponent<Turret>();

        startPos = transform.localPosition;

        // Subscribe to turret events
        turret.OnRotateTowardsTarget += DisableIdle;
        turret.OnRotateBackToRest += EnableIdleIfNoTarget;
    }

    void Update()
    {
        if (isIdle)
        {
            // Floating animation
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
        }
    }

    private void DisableIdle()
    {
        isIdle = false;
        transform.localPosition = startPos; // reset position
    }

    private void EnableIdleIfNoTarget()
    {
        // Enable idle ONLY if there is no target
        if (turret.enemyTarget == null)
            isIdle = true;
    }
}
