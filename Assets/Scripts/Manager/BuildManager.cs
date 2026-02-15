
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

    public GameObject StandardTurret;
    public GameObject Tower;
    public GameObject Sniper;
    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        Debug.Log("Turret selected to build: " + turretToBuild.prefab.name);
    }

    public void BuildTurretOn(Node node)
    {
        // Check if we have enough money to build the turret
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build " + turretToBuild.prefab.name);
            return;
        }

        // Deduct the cost of the turret from the player's money
        PlayerStats.Money -= turretToBuild.cost;
        Debug.Log("Built " + turretToBuild.prefab.name + ". Remaining money: " + PlayerStats.Money);


        // Check if a turret is selected to build   
        if (turretToBuild == null)
        {
            Debug.LogError("No turret selected to build.");
            return;
        }

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
