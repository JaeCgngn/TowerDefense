using UnityEngine;
using System.Collections;
public class WaveSpawner : MonoBehaviour
{

    public Transform SpawnPoint;
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;

    public float EnemySpawnInterval = 0.5f;

    public float countdown = 2f;
    public int waveNumber = 1;
    public float waveIndex = 0;

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
        Debug.Log("Wave Incoming! Wave: " + waveNumber);

        waveNumber++;

        for (int i = 0; i < waveNumber + 1; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemySpawnInterval);
        }

        waveNumber++;
    }


    void SpawnEnemy()
    {


        Debug.Log("Spawning Enemy!");
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);


    }


}
