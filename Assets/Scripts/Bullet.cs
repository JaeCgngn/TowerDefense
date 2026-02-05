using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    private Transform enemyTarget;

    private Vector3 moveDirection;

    [Header("Effects")]
    public GameObject hitEffect;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy bullet after its lifetime
    }

    public void Seek(Transform _target)
    {
        enemyTarget = _target;
    }


    void Update()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        BulletMovement();
    }





    void BulletMovement()
    {
        Vector3 dir = enemyTarget.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Logic for when the bullet hits a target


        Debug.Log("Bullet hit the target!");

        if (hitEffect != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(effect, 1f);
        }

        Destroy(gameObject);
    }
}
