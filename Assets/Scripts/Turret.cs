using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    public Transform enemyTarget;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private Shooting shooting;

    [Header("Settings")]
    [SerializeField] private float range = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private string enemyTag = "Enemy";

    [Header("Targeting")]
    [Range(-1f, 1f)]
    [SerializeField] private float dotThreshold = 0.7f;

    public event Action OnTargetAcquired;
    public event Action OnTargetLost;

    bool hadTarget;

    void Awake()
    {
        if (!shooting)
            shooting = GetComponent<Shooting>();
    }

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void Update()
    {
        bool hasTarget = enemyTarget != null;

        if (hasTarget)
            RotateTowardsTarget();

        if (hasTarget && !hadTarget)
            OnTargetAcquired?.Invoke();

        if (!hasTarget && hadTarget)
            OnTargetLost?.Invoke();

        hadTarget = hasTarget;
    }

    void RotateTowardsTarget()
    {
        Vector3 dir = enemyTarget.position - partToRotate.position;
        Quaternion look = Quaternion.LookRotation(dir);
        Quaternion yOnly = Quaternion.Euler(0f, look.eulerAngles.y, 0f);

        partToRotate.rotation = Quaternion.Lerp(
            partToRotate.rotation,
            yOnly,
            Time.deltaTime * rotationSpeed
        );
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortest = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject enemy in enemies)
        {
            Vector3 toEnemy = (enemy.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            float dot = Vector3.Dot(transform.forward, toEnemy);

            if (dot >= dotThreshold && distance < shortest)
            {
                shortest = distance;
                closest = enemy.transform;
            }
        }

        if (closest && shortest <= range)
        {
            enemyTarget = closest;
            shooting.StartFiring();
        }
        else
        {
            enemyTarget = null;
            shooting.StopFiring();
        }
    }
}
