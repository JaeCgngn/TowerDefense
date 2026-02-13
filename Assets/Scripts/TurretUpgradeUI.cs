using UnityEngine;

public class TurretUpgradeUI : MonoBehaviour
{
    public static TurretUpgradeUI instance;
    [SerializeField] private Transform panelChild;

    private TurretUpgradeManager selectedManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (panelChild == null)
            Debug.LogError("Panel child not assigned!");

        panelChild.gameObject.SetActive(false);
    }

    public void ShowAtTurret(Transform turretTransform, Vector3 offset)
    {
        panelChild.position = turretTransform.position + offset;
        panelChild.gameObject.SetActive(true);
    }

    public void Hide()
    {
        panelChild.gameObject.SetActive(false);
    }

    // Called when a turret is clicked
    public void SelectTurret(TurretUpgradeManager manager)
    {
        selectedManager = manager;
    }

    // Called by the Upgrade Button
    public void UpgradeSelectedTurret()
    {
        if (selectedManager != null)
            selectedManager.UpgradeTurret();
    }

    // Optional: Sell button
    public void SellSelectedTurret()
    {
        if (selectedManager != null)
            selectedManager.SellTurret();
        Hide();
    }
}
