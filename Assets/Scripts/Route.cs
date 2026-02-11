using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector3 gizmosPosition;

    // Draw Bezier Curve


    private void OnDrawGizmos()
    {
        Vector3 previousPoint = controlPoints[0].position;

        Gizmos.color = Color.green;

        for (float t = 0; t <= 1; t += 0.02f)
        {
            Vector3 currentPoint =
                Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        // Draw Control Wires
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);

        // Draw Control Points
        Gizmos.color = Color.red;      // Start
        Gizmos.DrawWireSphere(controlPoints[0].position, 0.2f);

        Gizmos.color = Color.yellow;   // Control points
        Gizmos.DrawWireSphere(controlPoints[1].position, 0.15f);
        Gizmos.DrawWireSphere(controlPoints[2].position, 0.15f);

        Gizmos.color = Color.blue;     // End
        Gizmos.DrawWireSphere(controlPoints[3].position, 0.2f);
    }



}
