using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    [Header("Turret Prefabs in Upgrade Order")]
    public GameObject[] turretPrefabs; // Assign in inspector (level 0 -> 1 -> 2, etc.)

    private GameObject currentTurret;
    private int currentLevel = 0;

    [Header("Turret Spawn Point")]
    public Transform turretParent; // Where the turret is instantiated

    /// <summary>
    /// Set the initial turret
    /// </summary>
    public void SetTurret(GameObject turret)
    {
        currentTurret = turret;
        currentLevel = 0; // reset level
    }

    /// <summary>
    /// Upgrade the turret to the next prefab in the array
    /// </summary>
    public void UpgradeTurret()
    {
        if (currentLevel >= turretPrefabs.Length - 1)
        {
            Debug.Log("Turret is already at max level!");
            return;
        }

        currentLevel++;

        // Destroy the old turret
        if (currentTurret != null)
            Destroy(currentTurret);

        // Instantiate the new upgraded turret
        currentTurret = Instantiate(turretPrefabs[currentLevel], turretParent.position, Quaternion.identity, turretParent);

        Debug.Log($"Turret upgraded to level {currentLevel}: {currentTurret.name}");
    }

    /// <summary>
    /// Optional: Sell the turret
    /// </summary>
    public void SellTurret()
    {
        if (currentTurret != null)
        {
            Destroy(currentTurret);
            currentTurret = null;
            currentLevel = 0;
            Debug.Log("Turret sold");
        }
    }

    /// <summary>
    /// Get current turret level
    /// </summary>
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
