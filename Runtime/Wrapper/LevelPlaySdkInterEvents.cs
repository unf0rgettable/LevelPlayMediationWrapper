using System;
using com.unity3d.mediation;
using LittleBitGames.Ads.AdUnits;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlaySdkInterEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;

        public LevelPlaySdkInterEvents(LevelPlayInterstitialAd levelPlayInterstitialAd)
        {
            levelPlayInterstitialAd.OnAdClicked += info => OnAdClicked?.Invoke(null, new AdInfo(info));
            levelPlayInterstitialAd.OnAdDisplayed += info => OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
            levelPlayInterstitialAd.OnAdLoaded += info => OnAdLoaded?.Invoke(null, new AdInfo(info));
            levelPlayInterstitialAd.OnAdLoadFailed += info => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
            levelPlayInterstitialAd.OnAdClosed += info => OnAdHidden?.Invoke(null, new AdInfo(info));
            levelPlayInterstitialAd.OnAdDisplayFailed += (errInfo) => OnAdDisplayFailed?.Invoke(null,
                new AdErrorInfo(errInfo.LevelPlayError), new AdInfo(errInfo.DisplayLevelPlayAdInfo));
        }
    }
}