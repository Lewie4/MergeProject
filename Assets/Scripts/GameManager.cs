using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public class SaveData
{
    public DateTime lastPlayed;
    public float softCurrency;
    public float hardCurrency;
    public List<int> itemLevels;
    public Dictionary<Upgrade, int> upgrades = new Dictionary<Upgrade, int>();
}

public class GameManager : Singleton<GameManager>
{
    private const string SAVEFILENAME = "SaveData";

    [Header("Save Data")]
    public SaveData saveData;

    [Header("Soft Currency")]
    [SerializeField] private float softCurrencyMultiplier = 1;
    [SerializeField] private TextMeshProUGUI softCurrencyText;
    [SerializeField] private TextMeshProUGUI softCurrencyPerSecond;

    [Header("Items")]
    [SerializeField] private float itemSpeedUpPercentage = 0.1f;
    [SerializeField] private Slider itemSpawnSlider;

    private float nextSpawnTime;

    private void Awake()
    {
        saveData = SaveManager.LoadData(SAVEFILENAME);

        UpdateSoftCurrencyText();

        foreach (Item item in ItemManager.Instance.items)
        {
            if (saveData.itemLevels.Count > 0)
            {
                item.SetLevel(saveData.itemLevels[0]);
                saveData.itemLevels.RemoveAt(0);
            }
        }
    }

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

    public void AddSoftCurrencyAmount(float currencyToAdd)
    {
        AddSoftCurrency(currencyToAdd * softCurrencyMultiplier);
    }

    public void AddSoftCurrencyTime(float seconds)
    {
        AddSoftCurrency(seconds* ItemManager.Instance.GetSoftCurrencyPerSecond());        
    }

    private void AddSoftCurrency(float amount)
    {
        saveData.softCurrency += amount;
        UpdateSoftCurrencyText();
    }

    public bool TryPurchase(float cost, Action success)
    {
        if (saveData.softCurrency >= cost)
        {
            saveData.softCurrency -= cost;
            UpdateSoftCurrencyText();
            success.Invoke();
            return true;
        }
        //Show not enough currency popup
        return false;
    }

    private void UpdateSoftCurrencyText()
    {
        softCurrencyText.text = saveData.softCurrency.ToString();
    }

    private void OnApplicationPause(bool pause)
    {        
        if(pause)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    [ContextMenu("Save Game")]
    public void SaveGame()
    { 
        saveData.lastPlayed = DateTime.Now.ToUniversalTime();

        saveData.itemLevels = new List<int>();
        foreach (Item item in ItemManager.Instance.items)
        {            
            saveData.itemLevels.Add(item.GetLevel());
        }

        SaveManager.SaveData(SAVEFILENAME, saveData);
    }

    [ContextMenu("Delete Save")]
    public void DeleteSave()
    {
        SaveManager.DeleteSave(SAVEFILENAME);
    }
}
