using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;

    private EnemyHealth enemyHealth;

    public void Initialize(EnemyHealth enemy)
    {
        enemyHealth = enemy;

        enemyHealth.OnHealthChanged += UpdateHealth;
        enemyHealth.OnDeath += HideHealthBar;

        UpdateHealth(enemyHealth.CurrentHP, enemyHealth.MaxHP);
    }

    private void UpdateHealth(int current, int max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;
    }

    private void HideHealthBar()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged -= UpdateHealth;
            enemyHealth.OnDeath -= HideHealthBar;
        }
    }

}
