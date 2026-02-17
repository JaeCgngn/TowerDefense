using UnityEngine;
using System.Collections;
using TMPro;

public class NodeUI : MonoBehaviour
{

    public GameObject ui;

    public TMP_Text priceText;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        UpdatePriceText(); 
        ui.SetActive(true);
    }


    public void Upgrade() // This method is called when the upgrade button is clicked
    {
        if (target == null) return;

        target.UpgradeTurret();
        UpdatePriceText();
        BuildManager.instance.DeselectNode();
        if (target.upgradeLevel >= 2)
        {
            Hide();
        }
    }

    private void UpdatePriceText()
    {
        if (target == null || target.blueprint == null)
        {
            priceText.text = "";
            return;
        }

        int price = 0;

        int nextLevel = target.upgradeLevel + 1;

        switch (nextLevel)
    {
        case 1:
            price = target.blueprint.secondUpgradeCost;
            break;
        case 2:
            price = target.blueprint.ThirdUpgradeCost;
            break;
        default:
            price = 0;
            break;
    }

        priceText.text = price > 0 ? $"Upgrade: {price}" : "Max Level";
    }

    public void Hide()
    {
        target = null;
        gameObject.SetActive(false);
    }

}
