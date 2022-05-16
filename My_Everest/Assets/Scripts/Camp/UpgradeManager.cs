using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Text UpgradeInfoText;
    public Text UpgradeCost;

    private int currentUpgrade;
    void Start()
    {
        currentUpgrade = 0;
        SetUpgrade();
    }

    public void Next()
    {
        currentUpgrade = Mathf.Min(UpgradeInfo.Upgrades.Count - 1, currentUpgrade + 1);
        SetUpgrade();
    }

    public void Back()
    {
        currentUpgrade = Mathf.Max(0, currentUpgrade - 1);
        SetUpgrade();
    }

    public void Upgrade()
    {
        var upg = UpgradeInfo.Upgrades[currentUpgrade];
        var inventory = ResourceManager.GetInventory();
        var cost = upg.GetCost();
        foreach(var res in cost.Keys)
        {
            if (inventory[res] < cost[res])
                return;
        }

        foreach (var res in cost.Keys)
        {
            ResourceManager.SaveResource(res, inventory[res] - cost[res]);
        }
        upg.SetUpgrate();
        upg.Lavel++;
        SetUpgrade();
        UpgradeInfo.Save();
    }

    private void SetUpgrade()
    {
        var upg = UpgradeInfo.Upgrades[currentUpgrade];
        UpgradeInfoText.text = $"{upg.Name} \n{upg.Description}";
        UpgradeCost.text = ToStr(upg.GetCost());
    }

    private string ToStr(Dictionary<Resource, int> resources)
    {
        var result = "Требуется для улучшения: ";
        foreach (var res in resources.Keys)
        {
            result += $"\n{res.ToString()}: {resources[res]}";
        }
        return result;
    }
}
