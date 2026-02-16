using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint blueprint;

    [HideInInspector]
    public bool isUpgraded = false;

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

            buildManager.SelectNode(this);
            return;
        }

        buildManager.SpawnBuildVFX(this);
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (blueprint == null)
        {
            Debug.LogError("No turret blueprint provided.");
            return;
        }

        // Check if we have enough money
        if (PlayerStats.Instance.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build " + blueprint.prefab.name);
            return;
        }

        this.blueprint = blueprint;

        // Deduct the cost
        PlayerStats.Instance.SpendMoney(blueprint.cost);
        Debug.Log("Built " + blueprint.prefab.name + ". Remaining money: " + PlayerStats.Instance.Money);

        // Instantiate the turret on the node
        GameObject turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        this.turret = turret;



    }

    public void UpgradeTurret()
    {
        if (blueprint == null)
        {
            Debug.LogError("No turret blueprint provided.");
            return;
        }

        // Check if we have enough money
        if (PlayerStats.Instance.Money < blueprint.UpgradeCost)
        {
            Debug.Log("Not enough money to upgrade " + blueprint.prefab.name);
            return;
        }

        // Deduct the cost
        PlayerStats.Instance.SpendMoney(blueprint.UpgradeCost);
        Debug.Log("Upgraded " + blueprint.prefab.name + ". Remaining money: " + PlayerStats.Instance.Money);

        // Destroy the old turret
        Destroy(turret);



        // Instantiate the upgraded turret on the node
        turret = Instantiate(blueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        this.turret = turret;

        isUpgraded = true;

        Debug.Log("Turret upgraded to " + blueprint.upgradedPrefab.name);

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
