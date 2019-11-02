using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade
{
    None,
    ItemSpawnLevel,
    ItemSpawnTime,
}

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private UpgradeScriptableObject itemSpawnLevels;
    [SerializeField] private UpgradeScriptableObject itemSpawnTimes;

    private Dictionary<Upgrade, int> upgrades = new Dictionary<Upgrade, int>();


    public int GetUpgradeLevel(Upgrade upgrade)
    {
        if (upgrades.TryGetValue(upgrade, out int level))
        {
            return level;
        }
        return 0;
    }

    public float GetUpgradeValue(Upgrade upgrade)
    {
        int upgradeLevel = GetUpgradeLevel(upgrade);

        float value = -1;

        switch (upgrade)
        {
            case Upgrade.ItemSpawnLevel:
                {
                    value = itemSpawnLevels.valueCost[upgradeLevel].x;
                    break;
                }
            case Upgrade.ItemSpawnTime:
                {
                    value = itemSpawnTimes.valueCost[upgradeLevel].x;
                    break;
                }
        }

        return value;
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

    public float GetCost(Upgrade upgrade, int offset = 0)
    {
        int upgradeLevel = GetUpgradeLevel(upgrade) + offset;

        float cost = -1;

        switch (upgrade)
        {
            case Upgrade.ItemSpawnLevel:
                {
                    cost = upgradeLevel >= itemSpawnLevels.valueCost.Count ? -1 : itemSpawnLevels.valueCost[upgradeLevel].y;
                    break;
                }
            case Upgrade.ItemSpawnTime:
                {
                    cost = upgradeLevel >= itemSpawnTimes.valueCost.Count ? -1 : itemSpawnTimes.valueCost[upgradeLevel].y;
                    break;
                }
        }

        return cost;
    }
}
