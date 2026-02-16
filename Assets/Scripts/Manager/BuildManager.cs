
using System.ComponentModel;
using System.Xml;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    [SerializeField] private GameObject buildVFX;
    [SerializeField] private float vfxDestroyTime = 2f;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }




    private TurretBlueprint turretToBuild;

    public NodeUI nodeUI;
    private Node selectedNode;
    public bool CanBuild { get { return turretToBuild != null; } }

    public void SelectNode(Node node)
    {

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        Debug.Log("Turret selected to build: " + turretToBuild.prefab.name);
        selectedNode = null;

        nodeUI.Hide();

        DeselectNode();
    }

    public void BuildTurretOn(Node node)
    {

        // Check if a turret is selected to build   
        if (turretToBuild == null)
        {
            Debug.LogError("No turret selected to build.");
            return;
        }

        // Check if we have enough money to build the turret
        if (PlayerStats.Instance.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build " + turretToBuild.prefab.name);
            return;
        }

        // Deduct the cost of the turret from the player's money
        PlayerStats.Instance.SpendMoney(turretToBuild.cost);
        Debug.Log("Built " + turretToBuild.prefab.name + ". Remaining money: " + PlayerStats.Instance.Money);

        // Instantiate the turret on the node
        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }

    public void SpawnBuildVFX(Node node)
    {
        if (buildVFX == null) return;

        GameObject vfx = Instantiate(
            buildVFX,
            node.transform.position + node.positionOffset,
            Quaternion.identity
        );

        Destroy(vfx, vfxDestroyTime);

    }


}
