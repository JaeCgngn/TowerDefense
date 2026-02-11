using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public FollowRoute follower;
    public Transform[] sceneRoutes;

    void Start()
    {
        follower.routes = sceneRoutes;
    }
}
