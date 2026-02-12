using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{

    public List<Transform> controlPoints = new List<Transform>();


    private void OnDrawGizmos()
    {
        if (controlPoints.Count < 4) return;

        Gizmos.color = Color.green;

        // Draw each segment
        for (int i = 0; i + 3 < controlPoints.Count; i += 3)
        {
            Vector3 previousPoint = controlPoints[i].position;

            for (float t = 0; t <= 1; t += 0.02f)
            {
                Vector3 currentPoint =
                    Mathf.Pow(1 - t, 3) * controlPoints[i + 0].position +
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[i + 1].position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[i + 2].position +
                    Mathf.Pow(t, 3) * controlPoints[i + 3].position;

                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }

            // Control wires
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(controlPoints[i + 0].position, controlPoints[i + 1].position);
            Gizmos.DrawLine(controlPoints[i + 2].position, controlPoints[i + 3].position);

            // Points
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controlPoints[i + 0].position, 0.2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(controlPoints[i + 1].position, 0.15f);
            Gizmos.DrawWireSphere(controlPoints[i + 2].position, 0.15f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(controlPoints[i + 3].position, 0.2f);
        }
    }

    public void AddSegment()
    {
        if (controlPoints.Count == 0)
        {
            Transform start = new GameObject("Point0").transform;
            start.parent = transform;
            start.position = Vector3.zero;

            Transform cp1 = new GameObject("Point1").transform;
            cp1.parent = transform;
            cp1.position = new Vector3(1, 0, 0);

            Transform cp2 = new GameObject("Point2").transform;
            cp2.parent = transform;
            cp2.position = new Vector3(2, 0, 0);

            Transform end = new GameObject("Point3").transform;
            end.parent = transform;
            end.position = new Vector3(3, 0, 0);

            controlPoints.AddRange(new[] { start, cp1, cp2, end });
            return;
        }

        Transform lastEnd = controlPoints[controlPoints.Count - 1];

        Transform newCP1 = new GameObject("CP1").transform;
        newCP1.parent = transform;
        newCP1.position = lastEnd.position + new Vector3(1, 0, 0);

        Transform newCP2 = new GameObject("CP2").transform;
        newCP2.parent = transform;
        newCP2.position = lastEnd.position + new Vector3(2, 0, 0);

        Transform newEnd = new GameObject("End").transform;
        newEnd.parent = transform;
        newEnd.position = lastEnd.position + new Vector3(3, 0, 0);

        controlPoints.AddRange(new[] { newCP1, newCP2, newEnd });
    }
}
