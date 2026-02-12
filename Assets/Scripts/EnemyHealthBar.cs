using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour
{
    // Prevents health bar flicker on spawn by delaying its visibility.
    // The bar is initially hidden and only shown after the first health update (0.5s delay).

    [SerializeField] private Slider healthSlider;
    [SerializeField] private CanvasGroup canvasGroup;
    private EnemyHealth enemyHealth;

    private bool hasShown = false;

    private Coroutine showCoroutine;

    [SerializeField] private float lerpSpeed = 5f;
    private float targetHealth;

    private void Awake()
    {
        // Auto-find CanvasGroup
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
    }

    public void Initialize(EnemyHealth enemy)
    {
        enemyHealth = enemy;

        enemyHealth.OnHealthChanged += UpdateHealth;
        enemyHealth.OnDeath += HideHealthBar;

        UpdateHealth(enemyHealth.CurrentHP, enemyHealth.MaxHP);
    }

    private void Update()
    {
        if (healthSlider.value != targetHealth)
        {
            healthSlider.value = Mathf.Lerp(
                healthSlider.value,
                targetHealth,
                lerpSpeed * Time.deltaTime
            );

            // Snap when very close (prevents endless tiny movement)
            if (Mathf.Abs(healthSlider.value - targetHealth) < 0.01f)
            {
                healthSlider.value = targetHealth;
            }
        }
    }

    private void UpdateHealth(int current, int max)
    {
        healthSlider.maxValue = max;
        targetHealth = current;


        // Only show once
        if (!hasShown)
        {
            hasShown = true;

            if (showCoroutine != null)
                StopCoroutine(showCoroutine);

            showCoroutine = StartCoroutine(ShowAfterDelay());
        }
    }

    private void HideHealthBar()
    {
        canvasGroup.alpha = 0;

    }

    private void OnDestroy()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged -= UpdateHealth;
            enemyHealth.OnDeath -= HideHealthBar;
        }
    }

    private IEnumerator ShowAfterDelay()
    {

        yield return new WaitForSeconds(0.5f);
        canvasGroup.alpha = 1;            // show after 0.5 seconds

    }
}
