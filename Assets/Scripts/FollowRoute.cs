using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FollowRoute : MonoBehaviour
{
    [SerializeField]
    public Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    public float speedModifier = 0.5f;
    private bool coroutineAllowed;

    public static event Action OnRouteFinished; 

    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Transform routeTransform = routes[routeNum];

        // Total segments = (points - 1) / 3
        int pointCount = routeTransform.childCount;
        int segments = (pointCount - 1) / 3;

        for (int s = 0; s < segments; s++) 
        {
            Vector3 p0 = routeTransform.GetChild(s * 3 + 0).position;
            Vector3 p1 = routeTransform.GetChild(s * 3 + 1).position;
            Vector3 p2 = routeTransform.GetChild(s * 3 + 2).position;
            Vector3 p3 = routeTransform.GetChild(s * 3 + 3).position;

            float tParam = 0f;

            while (tParam < 1f)
            {
                tParam += Time.deltaTime * speedModifier;

                Vector3 objectPosition =
                    Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

                transform.position = objectPosition;

                yield return null;
            }
        }

        // Finished entire route
        routeToGo++;
       

        if (routeToGo >= routes.Length)
        {
            Debug.Log("Enemy reached the end of the route!");

            OnRouteFinished?.Invoke();

            Destroy(gameObject);
            yield break;
        }

        coroutineAllowed = true;
    }
}
