
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret References")]
    public Transform enemyTarget;
    public Transform partToRotate;
    public string enemyTag = "Enemy";

    [Header("Turret Settings")]
    public float range = 10f;

    public float rotationSpeed = 5f;

    [Range(1f, -1f)]
    public float dotThreshold = 0.7f;

    Quaternion originalRotation;
    private Shooting shooting;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        originalRotation = transform.rotation; //store original rotation

    }

    private void Awake()
    {
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (enemyTarget != null)
        {
            TurretRotation();

        }
        else
        {
            ReturnToOriginalRotation();
        }

    }
    void TurretRotation()
    {

        //target direction
        Vector3 direction = enemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;

        //smooth rotation 
        Quaternion smoothRotation = Quaternion.Euler(0f, rotation.y, 0f);
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, smoothRotation, Time.deltaTime * rotationSpeed);

    }
    void ReturnToOriginalRotation()
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            originalRotation,
            rotationSpeed * Time.deltaTime
        );
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            float dot = Vector3.Dot(transform.forward, directionToEnemy);

            if (dot >= dotThreshold && distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            enemyTarget = nearestEnemy.transform;
            shooting.StartFiring();
        }
        else
        {
            enemyTarget = null;
            shooting.StopFiring();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        float clampedDot = Mathf.Clamp(dotThreshold, -1f, 1f);

        // Convert dot threshold to angle
        float halfFOV = Mathf.Acos(dotThreshold) * Mathf.Rad2Deg;

        Vector3 leftDir = Quaternion.Euler(0, -halfFOV, 0) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0, halfFOV, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftDir * range);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * range);
    }



}
