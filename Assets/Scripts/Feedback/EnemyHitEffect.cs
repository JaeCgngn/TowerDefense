using UnityEngine;
using System;

public class EnemyHitEffect : MonoBehaviour
{
    [Header("Hit Effect Settings")]
    [SerializeField] private Color hitColor = Color.white;
    [SerializeField] private float flashSpeed = 10f;

    private Renderer rend;
    private Color originalColor;
    private Material materialInstance;

    private float hitTimer = 0f;
    private bool isFlashing = false;

    private EnemyHealth enemy; // your health script

    private void Awake()
    {
        enemy = GetComponent<EnemyHealth>();

        rend = GetComponentInChildren<Renderer>();
        materialInstance = rend.material; // creates instance
        originalColor = materialInstance.color;
    }

    private void OnEnable()
    {
        enemy.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        enemy.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int current, int max)
    {
        // If health dropped, trigger flash
        isFlashing = true;
        hitTimer = 0f;
    }

    private void Update()
    {
        if (!isFlashing) return;

        hitTimer += Time.deltaTime * flashSpeed;

        // Lerp from hitColor back to original
        materialInstance.color = Color.Lerp(hitColor, originalColor, hitTimer);

        if (hitTimer >= 1f)
        {
            materialInstance.color = originalColor;
            isFlashing = false;
        }
    }
}
