using UnityEngine;

public class Node : MonoBehaviour
{

    private GameObject turret;

    public Color hoverColor;
    private Color startColor;
    private Renderer rend;

    void Start()
    {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

    }


    void OnMouseDown()
    {
        Debug.Log("Node Clicked: " + gameObject.name);

        if (turret != null)
        {
            Debug.Log("Turret already exists on this node.");
            return;
        }

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
