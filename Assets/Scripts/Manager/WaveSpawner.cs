using UnityEngine;
using System.Collections;
using System;

public class WaveSpawner : MonoBehaviour
{

    public Transform SpawnPoint;
    public Transform enemyPrefab;
    public float EnemySpawnInterval = 0.5f;


    [Header("Wave Settings")]
    public float countdown = 2f;
    public int waveNumber = 1;
    public float waveIndex = 0;
    private bool isSpawning;

    public AnimationCurve waveEnemyCountCurve;

    [SerializeField] private AnimationCurve waveDelayCurve;
    [SerializeField] private float minWaveDelay = 2f;
    [SerializeField] private float maxWaveDelay = 10f;
    public int maxEnemiesPerWave = 20;

    public static event Action<int> OnWaveStarted;
    public static event Action<float> OnCountdownUpdated;


    void Update()
    {

        if (isSpawning)
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            isSpawning = true;

            OnWaveStarted?.Invoke(waveNumber);
            return;
        }

        countdown -= Time.deltaTime;
        OnCountdownUpdated?.Invoke(countdown);
    }

    IEnumerator SpawnWave()
    {

        // Use curve to determine how many enemies to spawn
        float curveValue = waveEnemyCountCurve.Evaluate(waveNumber);
        int enemiesThisWave = Mathf.Clamp(Mathf.RoundToInt(curveValue), 1, maxEnemiesPerWave);


        for (int i = 0; i < enemiesThisWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemySpawnInterval);
        }

        Debug.Log("Curve Value: " + curveValue + "Wave Incoming! Wave: "
        + waveNumber + " Enemies: " + Mathf.Clamp(Mathf.RoundToInt(curveValue), 1, maxEnemiesPerWave));

        isSpawning = false;
        countdown = GetNextWaveDelay();
        waveNumber++;

    }

    float GetNextWaveDelay()
    {
        float curveValue = waveDelayCurve.Evaluate(waveNumber);

        // Map curve value to delay range
        float delay = Mathf.Lerp(minWaveDelay, maxWaveDelay, curveValue);

        Debug.Log("Next Wave Delay: " + delay);
        return delay;
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawning Enemy!");
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);

        // Transform enemyTransform = Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
        // EnemyHealth enemy = enemyTransform.GetComponent<EnemyHealth>();
        // UIManager.Instance.RegisterEnemy(enemy);

    }


}
