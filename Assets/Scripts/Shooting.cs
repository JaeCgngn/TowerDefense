using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("References ")]

    public GameObject bulletPrefab;
    public Transform firePoint;

    private Turret turret; // Set this to the enemy target later

    Coroutine fireCoroutine;
    bool isFiring = true;

    [Header("Shooter Settings")]
    public float fireRate = 1f;

    public float bulletLifetime = 3f;
    public float bulletSpeed = 20f;

    [Header("Burst Settings")]
    public int bulletsPerShot = 1;        // Number of bullets in the burst
    public float burstDelay = 0.1f;



    void Start()
    {

        //  StartCoroutine(FireRoutine());

    }

    private void Awake()
    {
        turret = GetComponent<Turret>();
    }


    public IEnumerator FireRoutine()
    {
        Debug.Log("Fire Routine Started");

        while (isFiring)
        {
            yield return StartCoroutine(FireBullet());
            yield return new WaitForSeconds(fireRate);
        }

        fireCoroutine = null;
    }

    public void StartFiring()
    {
        if (fireCoroutine != null) return;

        isFiring = true;
        fireCoroutine = StartCoroutine(FireRoutine());
    }

    public void StopFiring()
    {
        isFiring = false;
    }


    private IEnumerator FireBullet()
    {
        Debug.Log("Bullet Fired");

        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 bulletDirection = firePoint.forward;
            Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection);

            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            bullet.Seek(turret.enemyTarget);

            yield return new WaitForSeconds(burstDelay);
        }
    }

}