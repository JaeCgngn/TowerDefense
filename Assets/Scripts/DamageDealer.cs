using UnityEngine;
using UnityEngine.EventSystems;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int damage = 1;

    public int Damage => damage;

    public void DealDamage(EnemyHealth health)
    {
        if (health == null)
            return;

        Debug.Log($"Dealing {damage} damage to enemy with current HP: {health.CurrentHP}/{health.MaxHP}");

        health.TakeDamage(damage); // Call TakeDamage on the EnemyHealth component, passing the damage value
    }
}
