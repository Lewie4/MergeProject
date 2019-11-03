using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : Singleton<AdManager>
{
    [SerializeField] private AdProvider adProvider;

    public bool IsReady(string placement)
    {
        return adProvider.IsReady(placement);
    }

    public void ShowAd(string placement)
    {
        adProvider.ShowAd(placement);
    }


#if UNITY_EDITOR
    [ContextMenu("Test Interstitial")]
    private void TestInterstitial()
    {
        ShowAd("video");
    }

    [ContextMenu("Test Reward")]
    private void TestReward()
    {
        ShowAd("rewardedVideo");
    }
#endif
}
