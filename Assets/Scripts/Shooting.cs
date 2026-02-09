using System;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Turret turret;
    private Coroutine fireCoroutine;
    private bool isFiring = false;

    [Header("Shooter Settings")]
    public float fireRate = 1f;
    public float bulletLifetime = 3f;
    public float bulletSpeed = 20f;

    [Header("Damage Settings")]
    public int bulletDamage = 1;

    [Header("Burst Settings")]
    public int bulletsPerShot = 1;
    public float burstDelay = 0.1f;

    public event Action OnStartFiring;
    public event Action OnStopFiring;
    public event Action<GameObject> OnBulletFired;

    private void Awake()
    {
        turret = GetComponent<Turret>();
    }

    private void OnEnable()
    {
        turret.OnTargetAcquired += StartFiring;
        turret.OnTargetLost += StopFiring;

    }

    private void OnDisable()
    {
        turret.OnTargetAcquired -= StartFiring;
        turret.OnTargetLost -= StopFiring;
    }

    public void StartFiring()
    {
        if (turret.enemyTarget == null)
            return;

        if (fireCoroutine != null) return;

        isFiring = true;
        fireCoroutine = StartCoroutine(FireRoutine());
        OnStartFiring?.Invoke();
    }

    public void StopFiring()
    {
        isFiring = false;
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
        OnStopFiring?.Invoke();
    }

    private IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(fireRate);

        while (isFiring)
        {
            yield return StartCoroutine(FireBullet());
            yield return new WaitForSeconds(fireRate);
        }
        fireCoroutine = null;
    }

    private IEnumerator FireBullet()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Initialize(
                    turret.enemyTarget,
                    bulletDamage,
                    bulletSpeed,
                    bulletLifetime
                );

                bullet.OnHitTarget += HandleBulletHit;
                bullet.OnBulletDestroyed += HandleBulletDestroyed;
            }

            OnBulletFired?.Invoke(bulletObj);
            yield return new WaitForSeconds(burstDelay);
        }
    }

    private void HandleBulletHit(Transform target, int damage)
    {
        // Example: call enemy damage system
        EnemyHealth enemy = target.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    private void HandleBulletDestroyed(GameObject bullet)
    {
        // Example: spawn hit effects / pooling

        BulletEffectManager.Instance.SpawnHitEffect(bullet.transform.position);
        Debug.Log("Bullet destroyed: " + bullet.name);
    }
}
