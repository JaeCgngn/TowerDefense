using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{

    public Transform SpawnPoint;
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;

    public float EnemySpawnInterval = 0.5f;


    [Header("Wave Settings")]
    public float countdown = 2f;
    public int waveNumber = 1;
    public float waveIndex = 0;

    public AnimationCurve waveEnemyCountCurve;
    public int maxEnemiesPerWave = 20;

    void Update()
    {

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
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

        waveNumber++;
    }


    void SpawnEnemy()
    {


        Debug.Log("Spawning Enemy!");
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);


    }


}
