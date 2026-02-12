
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    [SerializeField] private GameObject buildVFX;
    [SerializeField] private float vfxDestroyTime = 2f;

    private GameObject turretToBuild;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret.prefab;
        Debug.Log("Turret selected to build: " + turretToBuild.name);
    }

    public GameObject GetTurretToBuild()
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

        Destroy(vfx, vfxDestroyTime);
    }





}
