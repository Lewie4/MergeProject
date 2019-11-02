using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade
{
    None,
    ItemSpawnLevel
}

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private UpgradeScriptableObject itemSpawnLevels;

    private Dictionary<Upgrade, int> upgrades = new Dictionary<Upgrade, int>();


    public int GetUpgradeLevel(Upgrade upgrade)
    {
        if (upgrades.TryGetValue(upgrade, out int level))
        {
            return level;
        }
        return 0;
    }

    public bool TryUpgrade(Upgrade upgrade)
    {
        float cost = GetCost(upgrade);

        return GameManager.Instance.TryPurchase(cost, () => UpgradeSuccessful(upgrade));
    }

    public void UpgradeSuccessful(Upgrade upgrade)
    {
        if(upgrades.ContainsKey(upgrade))
        {
            upgrades[upgrade] = upgrades[upgrade] + 1;
        }
        else
        {
            upgrades.Add(upgrade, 1);
        }
    }

    public float GetCost(Upgrade upgrade)
    {
        int upgradeLevel = GetUpgradeLevel(upgrade);

        float cost = -1;

        switch (upgrade)
        {
            case Upgrade.ItemSpawnLevel:
                {
                    cost = itemSpawnLevels.valueCost[upgradeLevel].y;
                    break;
                }
        }

        return cost;
    }
}
