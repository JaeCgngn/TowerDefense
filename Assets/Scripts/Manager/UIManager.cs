using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI countdownText;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Subscribe to static wave event — no reference needed
        WaveSpawner.OnWaveStarted += UpdateWaveUI;
        WaveSpawner.OnCountdownUpdated += UpdateCountdownUI;
        PlayerStats.Instance.OnMoneyChanged += UpdateGoldUI;
        UpdateGoldUI(PlayerStats.Instance.Money);

    }

    private void OnDestroy()
    {
        WaveSpawner.OnWaveStarted -= UpdateWaveUI;
        PlayerStats.Instance.OnMoneyChanged -= UpdateGoldUI;
    }

    void UpdateGoldUI(int newAmount)
    {
        goldText.text = "Gold: " + newAmount;
    }

    private void UpdateCountdownUI(float countdown)
    {
        // Display countdown rounded to 1 decimal
        countdownText.text = countdown.ToString("F1");
    }


    void UpdateWaveUI(int waveNumber)
    {
        waveText.text = waveNumber.ToString("D2");
    }
}
