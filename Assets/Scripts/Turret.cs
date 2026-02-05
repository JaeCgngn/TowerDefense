
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret References")]
    public Transform enemyTarget;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private Shooting shooting;

    [Header("Turret Settings")]
    [SerializeField] private float range = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private string enemyTag = "Enemy";

    [Header("Idle Animation")]
    [SerializeField] private float floatAmplitude = 0.1f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float idleRotationAmount = 2f;


    [Header("Targeting")]
    [Range(-1f, 1f)]
    [SerializeField] private float dotThreshold = 0.7f;

    private bool isIdle = false;
    private float idleTime;

    private Quaternion originalRotation;
    private Vector3 originalPosition;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);


    }

    private void Awake()
    {
        originalRotation = partToRotate.rotation;
        originalPosition = partToRotate.position;

        if (!shooting)
            shooting = GetComponent<Shooting>();
    }

    void Update()
    {
        if (enemyTarget != null)
        {
            isIdle = false;
            TurretRotation();
        }
        else
        {
            if (!isIdle)
            {
                ReturnToOriginalRotation();
            }
            else
            {
                IdleFloat();
            }
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

        if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
        {
            isIdle = true;
            idleTime = 0f;
        }


    }

    void IdleFloat()
    {
        idleTime += Time.deltaTime;

        // Vertical float
        float yOffset = Mathf.Sin(idleTime * floatSpeed) * floatAmplitude;
        transform.position = originalPosition + Vector3.up * yOffset;

        // Gentle rotation sway (optional but nice)
        float rotOffset = Mathf.Sin(idleTime * floatSpeed) * idleRotationAmount;
        transform.rotation = originalRotation * Quaternion.Euler(0f, rotOffset, 0f);


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
