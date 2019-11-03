using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdManager : Singleton<AdManager>
{
    [SerializeField] private AdProvider adProvider;

    private Action success;
    private Action fail;

    public bool IsReady(string placement)
    {
        return adProvider.IsReady(placement);
    }

    public void ShowAd(string placement, Action success, Action fail)
    {
        this.success = success;
        this.fail = fail;

        adProvider.ShowAd(placement);
    }

    public void Success()
    {
        if (success != null)
        {
            success.Invoke();
        }
    }

    public void Fail()
    {
        if (fail != null)
        {
            fail.Invoke();
        }
    }


#if UNITY_EDITOR
    [ContextMenu("Test Interstitial")]
    private void TestInterstitial()
    {
        ShowAd("video", null, null);
    }

    [ContextMenu("Test Reward")]
    private void TestReward()
    {
        ShowAd("rewardedVideo", null, null);
        //ShowAd("rewardedVideo", () => GameManager.Instance.AddSoftCurrency(99999), null);
    }
#endif
}
