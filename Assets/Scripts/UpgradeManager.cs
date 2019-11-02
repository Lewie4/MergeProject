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

    private UpgradeScriptableObject GetRelevantUpgrades(Upgrade upgrade)
    {
        switch (upgrade)
        {
            case Upgrade.ItemSpawnLevel:
                {
                    return itemSpawnLevels;
                }
            case Upgrade.ItemSpawnTime:
                {
                    return itemSpawnTimes;
                }
        }

        return null;
    }

    public float GetUpgradeValue(Upgrade upgrade)
    {
        int upgradeLevel = GetUpgradeLevel(upgrade);

        return GetRelevantUpgrades(upgrade).valueCost[upgradeLevel].x;
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

        return upgradeLevel >= GetRelevantUpgrades(upgrade).valueCost.Count ? -1 : GetRelevantUpgrades(upgrade).valueCost[upgradeLevel].y;
    }

    public Sprite GetSprite(Upgrade upgrade)
    {
        return GetRelevantUpgrades(upgrade).sprite;
    }

    public string GetDescription(Upgrade upgrade)
    {
        return GetRelevantUpgrades(upgrade).description;
    }
}
