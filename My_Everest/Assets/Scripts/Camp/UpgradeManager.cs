using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Text UpgradeInfoText;
    public Text UpgradeCost;

    private int currentUpgrade;
    private UpgradeInfo info;
    void Start()
    {
        info = new UpgradeInfo();
        currentUpgrade = 0;
        SetUpgrade();
    }

    public void Next()
    {
        currentUpgrade = Mathf.Min(info.Upgrades.Count - 1, currentUpgrade + 1);
        SetUpgrade();
    }

    public void Back()
    {
        currentUpgrade = Mathf.Max(0, currentUpgrade - 1);
        SetUpgrade();
    }

    private void SetUpgrade()
    {
        var upg = info.Upgrades[currentUpgrade];
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
