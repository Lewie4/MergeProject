﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TempUpgrade
{
    None,
    ItemSpawnLevel,
    ItemSpawnTime,
    ItemMergeLevel,
    GoldMult
}


public class TempUpgradeManager : Singleton<TempUpgradeManager>
{
    [SerializeField] List<TempUpgradeScriptableObject> tempUpgradeScriptableObjects;
    [SerializeField] Vector2 spawnTime;
    [SerializeField] float recheckTime;
    [SerializeField] GameObject button;
    [SerializeField] Image boostIcon;
    [SerializeField] Image boostTimeImage;

    private bool ready;
    private bool active;
    private float nextSpawnTime = -1;
    private float currentWaitTime;
    private TempUpgradeScriptableObject selectedTempUpgrade;
    private float remainingTime;

    public void Update()
    {
        if (!ready)
        {
            if (nextSpawnTime < 0)
            {
                nextSpawnTime = Random.Range(spawnTime.x, spawnTime.y);
            }

            currentWaitTime += Time.deltaTime;

            if (currentWaitTime >= nextSpawnTime)
            {
                currentWaitTime = 0;
                nextSpawnTime = -1;
                ready = true;

                selectedTempUpgrade = tempUpgradeScriptableObjects[Random.Range(0, tempUpgradeScriptableObjects.Count)];
                button.SetActive(true);
            }
        }
        else if (active)
        {
            remainingTime -= Time.deltaTime;

            boostTimeImage.fillAmount = remainingTime / selectedTempUpgrade.duration;

            if(remainingTime <= 0)
            {
                active = false;
                ready = false;
                selectedTempUpgrade = null;
                boostTimeImage.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateTempBoost()
    {
        active = true;
        remainingTime = selectedTempUpgrade.duration;
        button.SetActive(false);
        boostIcon.sprite = selectedTempUpgrade.sprite;
        boostTimeImage.fillAmount = 1;
        boostTimeImage.gameObject.SetActive(true);        
    }

    public float GetTempBoostMultiplier(TempUpgrade upgradeType)
    {
        if(active && selectedTempUpgrade != null && upgradeType == selectedTempUpgrade.upgradeType)
        {
            return selectedTempUpgrade.power;
        }

        switch (upgradeType)
        {
            case TempUpgrade.ItemSpawnLevel:
                return 0;
            default:
                return 1;
        }

    }
}
