using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Configs;
using Wrapper;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlaySdkAdUnitsFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly AdsConfig _adsConfig;

        public LevelPlaySdkAdUnitsFactory(ICoroutineRunner coroutineRunner, AdsConfig adsConfig)
        {
            _adsConfig = adsConfig;

            _coroutineRunner = coroutineRunner;
        }

        public IAdUnit CreateInterAdUnit() =>
            new LevelPlayInterAd(GetKey(_adsConfig.LevelPlaySettings.PlatformSettings.InterstitialAdunitId), _coroutineRunner);

        public IAdUnit CreateRewardedAdUnit() =>
            new LevelPlayRewardAd(GetKey(_adsConfig.LevelPlaySettings.PlatformSettings.RewardAdunitId), _coroutineRunner);
        
        public IAdUnit CreateBannerAdUnit() =>
            new LevelPlayBannerAd(GetKey(_adsConfig.LevelPlaySettings.PlatformSettings.BannerAdunitId), _coroutineRunner);

        private IAdUnitKey GetKey(string s)
        {
            var key = new AdUnitKey(s);

            if (!key.Validate()) throw new Exception($"LevelPlay ad unit key is invalid! Key: {s}");

            return key;
        }
    }
}