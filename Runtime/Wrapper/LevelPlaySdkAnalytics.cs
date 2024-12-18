using System;
using System.Collections.Generic;
using com.unity3d.mediation;
using LittleBitGames.Ads;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Collections.Extensions;
using LittleBitGames.Environment.Ads;
using LittleBitGames.Environment.Events;
using UnityEngine;
using UnityEngine.Scripting;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlaySdkAnalytics : IMediationNetworkAnalytics
    {
        private const string SdkSourceName = "LevelPlay_sdk";
        private const string Currency = "USD";

        private readonly IReadOnlyList<IAdUnit> _adUnits;

        public event Action<IDataEventAdImpression, AdType> OnAdRevenuePaidEvent;

        [Preserve]
        public LevelPlaySdkAnalytics(IAdsService adsService)
        {
            _adUnits = adsService.AdUnits;
            
            if (!_adUnits.Validate()) ThrowException();

            if (adsService.Initializer.IsInitialized)
            {
                Subscribe();
            }
            else
            {
                adsService.Initializer.OnMediationInitialized += Subscribe;
            }
        }

        private static void ThrowException() =>
            throw new Exception("Invalid list of ad units was provided to MaxSdkAnalytics");

        private void Subscribe()
        {
            //Debug.LogError(_adUnits.Count);
            foreach (var adUnit in _adUnits)
            {
                if (adUnit is LevelPlayInterAd)
                {
                    adUnit.Events.OnAdRevenuePaid += delegate(string s, IAdInfo info)
                    {
                        OnAdRevenuePaid(s, info, AdType.Inter);
                    };
                }
                
                if (adUnit is LevelPlayRewardAd)
                {
                    adUnit.Events.OnAdRevenuePaid += delegate(string s, IAdInfo info)
                    {
                        OnAdRevenuePaid(s, info, AdType.Rewarded);
                    };
                }
                
                if (adUnit is LevelPlayBannerAd)
                {
                    adUnit.Events.OnAdRevenuePaid += delegate(string s, IAdInfo info)
                    {
                        OnAdRevenuePaid(s, info, AdType.Inter);
                    };
                }
            }
        }

        private void OnAdRevenuePaid(string adUnitId, IAdInfo adInfo, AdType adType)
        {

            var adImpressionEvent = new DataEventAdImpression(
                new SdkSource(SdkSourceName),
                adInfo.NetworkName,
                adInfo.AdFormat,
                adInfo.AdUnitIdentifier,
                Currency,
                adInfo.Revenue);

            //Debug.LogError("OnAdRevenuePaidEVENT");
            OnAdRevenuePaidEvent?.Invoke(adImpressionEvent, adType);
        }
    }
}
