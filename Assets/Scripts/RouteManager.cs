using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public FollowRoute[] follower;
    public Transform[] sceneRoutes;

    void Start()
    {
        foreach (FollowRoute f in follower)
        {
            f.routes = sceneRoutes;
        }
    }
}
