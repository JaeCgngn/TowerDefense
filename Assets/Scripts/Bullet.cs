using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy bullet after its lifetime
    }

    public void Initialize(float bulletLifetime, float bulletSpeed) // Method to initialize bullet with specific lifetime and speed
    {
        lifeTime = bulletLifetime;
        speed = bulletSpeed;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move forward in local space
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
