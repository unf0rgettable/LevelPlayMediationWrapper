using System;
using com.unity3d.mediation;
using LevelPlayMediationWrapper.Runtime.Wrapper;
using LittleBitGames.Ads.AdUnits;

namespace Wrapper
{
    public class LevelPlaySDKBannerEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;

        public LevelPlaySDKBannerEvents(com.unity3d.mediation.LevelPlayBannerAd levelPlayBannerAd)
        {
            levelPlayBannerAd.OnAdClicked += info => OnAdClicked?.Invoke(null, new AdInfo(info));
            levelPlayBannerAd.OnAdDisplayed += info => OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
            levelPlayBannerAd.OnAdLoaded += info => OnAdLoaded?.Invoke(null, new AdInfo(info));
            levelPlayBannerAd.OnAdLoadFailed += info => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
            levelPlayBannerAd.OnAdCollapsed += info => OnAdHidden?.Invoke(null, new AdInfo(info));
            levelPlayBannerAd.OnAdDisplayFailed += (errInfo) => OnAdDisplayFailed?.Invoke(null,
                new AdErrorInfo(errInfo.LevelPlayError), new AdInfo(errInfo.DisplayLevelPlayAdInfo));
        }
    }
}