using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    public float speed = 5f;
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        // Set the initial target to the first waypoint
        target = Waypoints.points[0];

    }

    void Update()
    {

        MoveTowardsTarget();

    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;


        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            // Reached the final waypoint
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];

    }

}
