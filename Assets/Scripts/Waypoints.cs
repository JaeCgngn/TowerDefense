using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] points;


    void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            // Populate the points array with the child transforms
            points[i] = transform.GetChild(i);

            Debug.Log(points[i].name);


        }
    }
}
