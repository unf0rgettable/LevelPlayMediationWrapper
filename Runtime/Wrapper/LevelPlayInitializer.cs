using System;
using System.Collections.Generic;
using com.unity3d.mediation;
using LevelPlayMediationWrapper.Runtime.Wrapper;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment;
using LittleBitGames.Environment.Ads;
using UnityEngine;

namespace YandexMobileAds.Wrapper
{
    public class LevelPlayInitializer : IMediationNetworkInitializer
    {
        public event Action OnMediationInitialized;
        
        private readonly AdsConfig _config;
        
        public LevelPlayInitializer(AdsConfig config) => _config = config;
        
        private bool IsDebugMode => _config.Mode is ExecutionMode.Debug;
        
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            List<LevelPlayAdFormat> levelPlayAdFormats = new();
            if (_config.IsRewarded)
            {
                levelPlayAdFormats.Add(LevelPlayAdFormat.REWARDED);
            }
            if (_config.IsBanner)
            {
                levelPlayAdFormats.Add(LevelPlayAdFormat.BANNER);
            }
            if (_config.IsInter)
            {
                levelPlayAdFormats.Add(LevelPlayAdFormat.INTERSTITIAL);
            }
            
            
            LevelPlay.Init(_config.LevelPlaySettings.PlatformSettings.AppKey,null, levelPlayAdFormats.ToArray());

            LevelPlay.OnInitSuccess += configuration =>
            {
                IronSourceEventsOnonSdkInitializationCompletedEvent();
            };
            
            LevelPlay.OnInitFailed += configuration =>
            {
                Debug.LogError("INIT FAILED " + configuration.ErrorMessage);
            };
        }

        private void IronSourceEventsOnonSdkInitializationCompletedEvent()
        {
            IsInitialized = true;
            OnMediationInitialized?.Invoke();
        }
    }
}