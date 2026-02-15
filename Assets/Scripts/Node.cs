using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public GameObject turret;

    public Vector3 positionOffset;
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;

    BuildManager buildManager;

    void Start()
    {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;



        if (!buildManager.CanBuild) // Check if we can build
        {

            return;
        }

        if (turret != null) // Check if there is already a turret on this node
        {
            Debug.Log("Turret exists: " + turret.name);
            return;
        }
        buildManager.BuildTurretOn(this);
        buildManager.SpawnBuildVFX(this);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Exited Node: " + gameObject.name);
        rend.material.color = startColor;
    }





}
