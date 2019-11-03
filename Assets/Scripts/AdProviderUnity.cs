using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdProviderUnity : AdProvider
{
#if UNITY_IOS
   private string gameId = "3349972";
#elif UNITY_ANDROID
    private string gameId = "3349973";
#endif

    protected override void Start()
    {
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    public override bool IsReady(string placement)
    {
        return Monetization.IsReady(placement);
    }

    public override void ShowAd(string placement)
    {
        ShowAdCallbacks options = new ShowAdCallbacks
        {
            finishCallback = HandleShowResult
        };
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placement) as ShowAdPlacementContent;
        ad.Show(options);
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
