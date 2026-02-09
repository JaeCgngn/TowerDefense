
using UnityEngine;

public class BuildManager : MonoBehaviour
{

     public static BuildManager instance;

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





}
