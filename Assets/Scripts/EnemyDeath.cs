using UnityEngine;
using UnityEngine.EventSystems;
public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth health;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>(); // Get reference to EnemyHealth component
        health.OnDeath += HandleDeath; // Subscribe to death event
    }

    private void OnDestroy()
    {
        health.OnDeath -= HandleDeath; // Unsubscribe to prevent memory leaks
    }

    private void HandleDeath()
    {
        if (health == null || health.CurrentHP > 0) return;
        Destroy(gameObject);
    }
}
