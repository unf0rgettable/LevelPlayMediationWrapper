using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment.Ads;
using YandexMobileAds.Wrapper;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlayAdsServiceBuilder : IAdsServiceBuilder
    {
        private readonly LevelPlaySdkAdUnitsFactory _adUnitsFactory;
        private readonly LevelPlayInitializer _initializer;

        private IAdUnit _inter, _rewarded, _banner;
        private AdsConfig _adsConfig;

        public IMediationNetworkInitializer Initializer => _initializer;

        public LevelPlayAdsServiceBuilder(AdsConfig adsConfig, ICoroutineRunner coroutineRunner)
        {
            _adsConfig = adsConfig;
            _adUnitsFactory = new LevelPlaySdkAdUnitsFactory(coroutineRunner, adsConfig);
            _initializer = new LevelPlayInitializer(adsConfig);
        }

        public IAdsService QuickBuild()
        {
            if (!string.IsNullOrEmpty(_adsConfig.LevelPlaySettings.PlatformSettings.InterstitialAdunitId) && _adsConfig.IsInter) 
                BuildInterAdUnit();
            if (!string.IsNullOrEmpty(_adsConfig.LevelPlaySettings.PlatformSettings.RewardAdunitId) && _adsConfig.IsRewarded) 
                BuildRewardedAdUnit();
            if (!string.IsNullOrEmpty(_adsConfig.LevelPlaySettings.PlatformSettings.BannerAdunitId) && _adsConfig.IsRewarded) 
                BuildBannerAdUnit();
            
            return GetResult();
        }

        public void BuildInterAdUnit() =>
            _inter = _adUnitsFactory.CreateInterAdUnit();

        public void BuildRewardedAdUnit() =>
            _rewarded = _adUnitsFactory.CreateRewardedAdUnit();

        public void BuildBannerAdUnit() => 
            _banner = _adUnitsFactory.CreateBannerAdUnit();
        
        public IAdsService GetResult() => new AdsService(_initializer, _inter, _rewarded, _banner);
    }
}