using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Soft Currency")]
    [SerializeField] private double softCurrency;
    [SerializeField] private float softCurrencyMultiplier = 1;
    [SerializeField] private TextMeshProUGUI softCurrencyText;

    [Header("Hard Currency")]
    [SerializeField] private int hardCurrency;

    [Header("Items")]
    [SerializeField] private float itemSpawnTime = 5f;
    [SerializeField] private int itemSpawnLevel;
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
            ItemManager.Instance.SpawnItem(itemSpawnLevel);
        }

        itemSpawnSlider.value = nextSpawnTime / itemSpawnTime;
    }

    public void SpeedUpSpawn()
    {
        nextSpawnTime += itemSpawnTime * itemSpeedUpPercentage;
    }

    public void CheckSoftCurrency()
    {
        ItemManager.Instance.CheckSoftCurrency(Time.deltaTime);
    }

    public void AddSoftCurrency(double currencyToAdd)
    {
        softCurrency += currencyToAdd * softCurrencyMultiplier;

        if (softCurrencyText != null)
        {
            softCurrencyText.text = softCurrency.ToString();
        }
    }
}
