using UnityEngine;

public class BulletEffectManager : MonoBehaviour
{
    public static BulletEffectManager Instance;

    public GameObject hitEffectPrefab;
    public float effectLifetime = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnHitEffect(Vector3 pos)
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, pos, Quaternion.identity);
            Destroy(effect, effectLifetime);
        }
    }
}
