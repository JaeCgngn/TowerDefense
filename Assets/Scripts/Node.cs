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

    public int upgradeLevel = 0;

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

            buildManager.SelectNode(this); // Select this node to show the upgrade/sell UI
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

        if (upgradeLevel >= 2) // Assuming we have 3 levels: base, second upgrade, third upgrade
        {
            Debug.Log("Turret already at max level!");
            return;
        }

        int cost = 0;
        GameObject nextPrefab = null;

        if (upgradeLevel == 0) // First upgrade
        {
            cost = blueprint.secondUpgradeCost;
            nextPrefab = blueprint.secondUpgradedPrefab;
        }
        else if (upgradeLevel == 1) // Second upgrade
        {
            cost = blueprint.ThirdUpgradeCost;
            nextPrefab = blueprint.thirdUpgradePrefab;
        }


        if (PlayerStats.Instance.Money < cost) // Check if we have enough money for the upgrade
        {
            Debug.Log("Not enough money to upgrade.");
            return;
        }

        PlayerStats.Instance.SpendMoney(cost); // Deduct the upgrade cost

        BuildManager.instance.DeselectNode(); 
        Destroy(turret);

        turret = Instantiate(nextPrefab, GetBuildPosition(), Quaternion.identity);

        upgradeLevel++;

        AudioManager.Instance.PlayUpgrade();

        Debug.Log("Turret upgraded to level " + (upgradeLevel + 1));
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
