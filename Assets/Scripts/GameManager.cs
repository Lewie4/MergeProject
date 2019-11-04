using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager>
{
    [Header("Soft Currency")]
    [SerializeField] private float softCurrency;
    [SerializeField] private float softCurrencyMultiplier = 1;
    [SerializeField] private TextMeshProUGUI softCurrencyText;
    [SerializeField] private TextMeshProUGUI softCurrencyPerSecond;

    [Header("Hard Currency")]
    [SerializeField] private int hardCurrency;

    [Header("Items")]
    [SerializeField] private float itemSpeedUpPercentage = 0.1f;
    [SerializeField] private Slider itemSpawnSlider;

    private float nextSpawnTime;

    private void Update()
    {
        CheckItemSpawn();
        CheckSoftCurrency();
    }

    private void CheckItemSpawn()
    {
        nextSpawnTime += Time.deltaTime;
        float spawnTime = UpgradeManager.Instance.GetUpgradeValue(Upgrade.ItemSpawnTime) / BoostManager.Instance.GetBoostMultiplier(Boost.ItemSpawnTime);

        if (nextSpawnTime >= spawnTime)
        {
            nextSpawnTime -= spawnTime;
            ItemManager.Instance.SpawnItem((int)UpgradeManager.Instance.GetUpgradeValue(Upgrade.ItemSpawnLevel) + ((int)BoostManager.Instance.GetBoostMultiplier(Boost.ItemSpawnLevel)));
        }

        itemSpawnSlider.value = nextSpawnTime / spawnTime;
    }

    public void SpeedUpSpawn()
    {
        nextSpawnTime += UpgradeManager.Instance.GetUpgradeValue(Upgrade.ItemSpawnTime) * itemSpeedUpPercentage;
    }

    public void CheckSoftCurrency()
    {
        ItemManager.Instance.CheckSoftCurrency(Time.deltaTime);
        softCurrencyPerSecond.text = ItemManager.Instance.GetSoftCurrencyPerSecond() + "/second";
    }    

    public void AddSoftCurrency(float currencyToAdd)
    {
        softCurrency += currencyToAdd * softCurrencyMultiplier;

        UpdateSoftCurrencyText();
    }

    public void AddSoftCurrencyTime(float seconds)
    {
        softCurrency += seconds * ItemManager.Instance.GetSoftCurrencyPerSecond();
        UpdateSoftCurrencyText();
    }

    public bool TryPurchase(float cost, Action success)
    {
        if (softCurrency >= cost)
        {
            softCurrency -= cost;
            UpdateSoftCurrencyText();
            success.Invoke();
            return true;
        }
        //Show not enough currency popup
        return false;
    }

    private void UpdateSoftCurrencyText()
    {
        softCurrencyText.text = softCurrency.ToString();
    }
}
