using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Boost
{
    None,
    ItemSpawnLevel,
    ItemSpawnTime,
    ItemMergeLevel,
    GoldMult
}

public class BoostManager : Singleton<BoostManager>
{
    [SerializeField] List<BoostScriptableObject> Boosts;
    [SerializeField] Vector2 spawnTime;
    [SerializeField] float recheckTime;
    [SerializeField] GameObject button;
    [SerializeField] Image boostIcon;
    [SerializeField] Image boostTimeImage;

    private bool ready;
    private bool active;
    private bool adWatched;
    private float nextSpawnTime = -1;
    private float currentWaitTime;
    private BoostScriptableObject selectedBoost;
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

                selectedBoost = Boosts[Random.Range(0, Boosts.Count)];
                button.SetActive(true);
            }
        }
        else if (active)
        {
            remainingTime -= Time.deltaTime;

            boostTimeImage.fillAmount = remainingTime / (adWatched ? selectedBoost.adDuration : selectedBoost.duration);

            if(remainingTime <= 0)
            {
                active = false;
                ready = false;
                adWatched = false;
                selectedBoost = null;
                boostTimeImage.gameObject.SetActive(false);
            }
        }
    }

    public void WatchAdForBoost()
    {
        AdManager.Instance.ShowAd(AdManager.REWARD, () => ActivateBoost(true));
    }

    public void ActivateBoost(bool advertWatched = false)
    {
        active = true;
        adWatched = advertWatched;
        remainingTime = adWatched ? selectedBoost.adDuration : selectedBoost.duration;
        button.SetActive(false);
        boostIcon.sprite = selectedBoost.sprite;
        boostTimeImage.fillAmount = 1;
        boostTimeImage.gameObject.SetActive(true);        
    }

    public float GetBoostMultiplier(Boost upgradeType)
    {
        if(active && selectedBoost != null && upgradeType == selectedBoost.upgradeType)
        {
            return adWatched ? selectedBoost.adPower : selectedBoost.power;
        }

        switch (upgradeType)
        {
            case Boost.ItemSpawnLevel:
            case Boost.ItemMergeLevel:
                return 0;
            default:
                return 1;
        }

    }
}
