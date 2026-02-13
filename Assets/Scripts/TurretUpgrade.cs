using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    public Vector3 uiOffset = new Vector3(0, 1.5f, 0);

    [Header("Hover Colors")]
    public Color hoverColor = Color.green;
    private Color startColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            startColor = rend.material.color;
        else
            Debug.LogWarning("Renderer not found on turret: " + gameObject.name);
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Turret: " + gameObject.name);

        if (rend != null)
            rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Exited Turret: " + gameObject.name);

        if (rend != null)
            rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        Debug.Log("Turret clicked: " + gameObject.name);

        if (TurretUpgradeUI.instance != null)
        {
            // Show the UI above this turret
            TurretUpgradeUI.instance.ShowAtTurret(transform, uiOffset);
        }
    }
}
