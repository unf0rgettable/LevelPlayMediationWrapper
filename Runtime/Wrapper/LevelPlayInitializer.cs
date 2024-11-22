using System;
using System.Collections.Generic;
using LevelPlayMediationWrapper.Runtime.Wrapper;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment;
using LittleBitGames.Environment.Ads;

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
            IronSourceEvents.onSdkInitializationCompletedEvent += IronSourceEventsOnonSdkInitializationCompletedEvent;
            
            IronSource.Agent.init(_config.LevelPlaySettings.PlatformSettings.AppKey);
            IronSource.Agent.setAdaptersDebug(IsDebugMode);
            IronSource.Agent.setManualLoadRewardedVideo(true);
        }

        private void IronSourceEventsOnonSdkInitializationCompletedEvent()
        {
            IsInitialized = true;
            OnMediationInitialized?.Invoke();
            
            IronSourceEvents.onSdkInitializationCompletedEvent -= IronSourceEventsOnonSdkInitializationCompletedEvent;
        }
    }
}