using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("References ")]

    public GameObject bulletPrefab;
    public Transform firePoint;

    Coroutine fireCoroutine;
    bool isFiring = true;

    [Header("Shooter Settings")]
    public float fireRate = 1f;

    public float speed = 20f;

    [Header("Spread Settings")]
    public int bulletsPerShot = 1;      // Number of bullets fired each time
    public float spreadAngle = 10f;



    void Start()
    {

        //  StartCoroutine(FireRoutine());

    }




    public IEnumerator FireRoutine()
    {
        Debug.Log("Fire Routine Started");

        while (isFiring)
        {
            FireBullet();
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


    void FireBullet()
    {
        Debug.Log("Bullet Fired");

        for (int i = 0; i < bulletsPerShot; i++) // Fire multiple bullets per shot
        {
            // Calculate random spread angle
            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            // Apply spread to rotation
            Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, 0, randomAngle);


            Instantiate(
                bulletPrefab,
                firePoint.position,
                spreadRotation
            );
        }
    }

}