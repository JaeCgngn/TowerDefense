using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using System.Data;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;


    public int MaxHP => maxHP;
    public int CurrentHP { get; private set; }



    // Events
    public event Action<int, int> OnHealthChanged; // current, max
    public event Action OnDeath;

    [SerializeField] private GameObject healthBarPrefab;
    private GameObject spawnedUI;
    private bool isDead;

    private void Start()
    {
        SpawnHealthUI();
    }

    private void SpawnHealthUI()
    {
        if (healthBarPrefab == null)
        {
            Debug.LogError("HealthBar prefab is NOT assigned!");
            return;
        }

        spawnedUI = Instantiate(healthBarPrefab);

        EnemyHealthBar ui = spawnedUI.GetComponentInChildren<EnemyHealthBar>();
        if (ui == null)
        {
            Debug.LogError("EnemyHealthBar script missing from prefab!");
            Destroy(spawnedUI);
            return;
        }

        ui.Initialize(this);

        FollowTargetWorld follow = spawnedUI.GetComponent<FollowTargetWorld>();
        if (follow == null)
        {
            Debug.LogError("FollowTargetWorld script missing from prefab root!");
            Destroy(spawnedUI);
            return;
        }

        follow.target = transform;

        OnDeath += DestroyHealthUI;
    }

    private void DestroyHealthUI()
    {
        if (spawnedUI != null)
            Destroy(spawnedUI);
    }

    private void OnDisable()
    {
        OnDeath -= DestroyHealthUI;
    }

    private void OnDestroy()
    {
        DestroyHealthUI();
    }

    private void Awake()
    {
        CurrentHP = maxHP;
        isDead = false;
    }

    public void TakeDamage(int amount)
    {

        if (isDead || amount <= 0) return;


        CurrentHP -= amount;
        CurrentHP = Mathf.Max(CurrentHP, 0);

        OnHealthChanged?.Invoke(CurrentHP, maxHP); // Notify listeners of health change

        Debug.Log($"Enemy took {amount} damage, current HP: {CurrentHP}/{maxHP}");

        if (CurrentHP == 0)
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }

}
