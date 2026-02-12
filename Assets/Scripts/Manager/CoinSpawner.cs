using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinsToSpawn = 1;

    private EnemyHealth enemy;

     private void Awake()
    {
        enemy = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        enemy.OnDeath += SpawnCoins;
    }

    private void OnDisable()
    {
        enemy.OnDeath -= SpawnCoins;
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinsToSpawn; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
