using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint Turret;
    public TurretBlueprint Tower;
    public TurretBlueprint Sniper;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectTurret()
    {
        Debug.Log("Turret Selected");
        buildManager.SelectTurretToBuild(Turret);
    }

    public void SelectTower()
    {
        Debug.Log("Tower Selected");
        buildManager.SelectTurretToBuild(Tower);
    }

    public void SelectSniper()
    {
        Debug.Log("Sniper Selected");
        buildManager.SelectTurretToBuild(Sniper);
    }

}
