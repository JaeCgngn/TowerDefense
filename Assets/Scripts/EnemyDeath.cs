using UnityEngine;
using UnityEngine.EventSystems;
public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth health;
    // public GameObject coinPrefab; 
    // public int coinsToDrop = 5; 

    private void Awake()
    {
        health = GetComponent<EnemyHealth>(); // Get reference to EnemyHealth component
        health.OnDeath += HandleDeath; // listen to the OnDeath event
    }

    private void OnDestroy()
    {
        health.OnDeath -= HandleDeath; //Destroy listener when this object is destroyed
    }

    private void HandleDeath()
    {
        if (health == null || health.CurrentHP > 0) return; // Check if health is null or still alive
        // DropCoin(); 
        Destroy(gameObject);
    }

    // public void DropCoin()
    // {
    //     for (int i = 0; i < coinsToDrop; i++)
    //     {
    //         Instantiate(coinPrefab, transform.position, Quaternion.identity);
    //     }
    // }
}
