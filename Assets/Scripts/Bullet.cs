using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    private Transform target;
    private int damage;

    public event Action<Transform, int> OnHitTarget;
    public event Action<Vector3> OnBulletDestroyed;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(Transform _target, int _damage, float _speed, float _lifeTime)
    {
        target = _target;
        damage = _damage;
        speed = _speed;
        lifeTime = _lifeTime;
    }

    private void Update()
    {
        if (target == null)
        {
            DestroyBullet();
            return;
        }

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        OnHitTarget?.Invoke(target, damage);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Vector3 pos = transform.position;
        OnBulletDestroyed?.Invoke(pos);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnHitTarget = null;
        OnBulletDestroyed = null;
    }
}
