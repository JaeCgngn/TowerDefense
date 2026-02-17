
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

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }



    public void SpawnBuildVFX(Node node)
    {
        if (buildVFX == null) return;

        GameObject vfx = Instantiate(
            buildVFX,
            node.transform.position + node.positionOffset,
            Quaternion.identity
        );
        AudioManager.Instance.PlayInsertTurret();

        Destroy(vfx, vfxDestroyTime);

    }


}
