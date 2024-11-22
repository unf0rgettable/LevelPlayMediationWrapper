using System;
using com.unity3d.mediation;
using LittleBitGames.Ads.AdUnits;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlaySdkRewardEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;
        
        public LevelPlaySdkRewardEvents(LevelPlayRewardedAd rewardedAd)
        {
            rewardedAd.OnAdClicked += info => OnAdClicked?.Invoke(null, new AdInfo(info));
            rewardedAd.OnAdDisplayed += info => OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
            rewardedAd.OnAdLoaded += info => OnAdLoaded?.Invoke(null, new AdInfo(info));
            rewardedAd.OnAdLoadFailed += info => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
            rewardedAd.OnAdClosed += info => OnAdHidden?.Invoke(null, new AdInfo(info));
            rewardedAd.OnAdDisplayFailed += (errInfo) => OnAdDisplayFailed?.Invoke(null,
                new AdErrorInfo(errInfo.LevelPlayError), new AdInfo(errInfo.DisplayLevelPlayAdInfo));
        }
    }
}