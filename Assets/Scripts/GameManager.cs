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
    [SerializeField] private float itemSpawnTime = 5f;
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

        if (nextSpawnTime >= itemSpawnTime)
        {
            nextSpawnTime -= itemSpawnTime;
            ItemManager.Instance.SpawnItem(UpgradeManager.Instance.GetUpgradeLevel(Upgrade.ItemSpawnLevel));
        }

        itemSpawnSlider.value = nextSpawnTime / itemSpawnTime;
    }

    public void SpeedUpSpawn()
    {
        nextSpawnTime += itemSpawnTime * itemSpeedUpPercentage;
    }

    public void CheckSoftCurrency()
    {
        float SCPS = ItemManager.Instance.CheckSoftCurrency(Time.deltaTime);
        softCurrencyPerSecond.text = SCPS + "/second";
    }

    public void AddSoftCurrency(float currencyToAdd)
    {
        softCurrency += currencyToAdd * softCurrencyMultiplier;

        if (softCurrencyText != null)
        {
            softCurrencyText.text = softCurrency.ToString();
        }
    }

    public bool TryPurchase(float cost, Action success)
    {
        if (softCurrency >= cost)
        {
            softCurrency -= cost;
            success.Invoke();
            return true;
        }
        //Show not enough currency popup
        return false;
    }
}
