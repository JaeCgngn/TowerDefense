using UnityEngine;

public class Node : MonoBehaviour
{

    private GameObject turret;

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


     void OnMouseDown()
    {
        Debug.Log("Node Clicked: " + gameObject.name);

        if (turret != null)
        {
            Debug.Log("Turret already exists on this node.");
            return;
        }

        GameObject turretToBuild = buildManager.GetTurretToBuild();

        if (turretToBuild == null)
        {
            Debug.Log("No turret selected to build.");
            return;
        }

        turret = Instantiate(
            turretToBuild,
            transform.position + positionOffset,
            Quaternion.identity
        );

        Debug.Log("Turret placed on node: " + gameObject.name);
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Node: " + gameObject.name);

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Exited Node: " + gameObject.name);
        rend.material.color = startColor;
    }

    



}
