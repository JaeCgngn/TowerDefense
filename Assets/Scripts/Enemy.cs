using UnityEngine;
using System.Collections.Generic;
//using System.Security.Cryptography.X509Certificates;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 10;
    public int currentHealth;
    public int damage = 10;

    [Header("Rewards")]
    public int rewardCoins = 5;

    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
        
    }

    void Start()
    {
        currentHealth = health;
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GiveRewards();
        Destroy(gameObject);
    }

    void GiveRewards()
    {
        gameManager.AddCoins(rewardCoins);
        Debug.Log($"Player receives {rewardCoins} coins!");
    }

}
