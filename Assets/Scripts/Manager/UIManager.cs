using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public int currentGold = 0;
    public int coinValue = 5;

    [Header("UI")]
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateGoldUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemy.OnDeath += HandleEnemyDeath;
    }

    void HandleEnemyDeath()
    {
        AddGold(coinValue);
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
        Debug.Log("Added " + amount + " gold. Current gold: " + currentGold);
    }

    void UpdateGoldUI()
    {
        goldText.text = "Gold: " + currentGold;
    }
}
