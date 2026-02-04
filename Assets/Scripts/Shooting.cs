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

    public float bulletLifetime = 3f;
    public float bulletSpeed = 20f;

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

        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 spreadDirection =
                Quaternion.Euler(
                    0f, // X axis = vertical spread
                    Random.Range(-spreadAngle, spreadAngle), // Y axis = horizontal spread
                    0f  // Z axis = no roll

                ) * firePoint.forward;

            Quaternion spreadRotation = Quaternion.LookRotation(spreadDirection);

            GameObject bulletObj = Instantiate(
                bulletPrefab,
                firePoint.position,
                spreadRotation
            );

            // Set bullet lifetime and speed from the shooter script
            bulletObj.GetComponent<Bullet>().Initialize(bulletLifetime, bulletSpeed);
        }
    }


}