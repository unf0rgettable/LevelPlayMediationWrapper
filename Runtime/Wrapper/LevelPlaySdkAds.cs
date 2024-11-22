using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment.Ads;
using UnityEngine;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlaySdkAds
    {
        private readonly LevelPlayAdsServiceBuilder _builder;
        private readonly AdsConfig _adsConfig;
        private readonly ICreator _creator;
        private IAdsService _adsService;

        public LevelPlaySdkAds(ICreator creator, ICoroutineRunner coroutineRunner)
        {
            _creator = creator;
            
            _adsConfig = Resources.Load<AdsConfig>(AdsConfig.PathInResources);
            
            _builder = creator.Instantiate<LevelPlayAdsServiceBuilder>(_adsConfig);
        }

        public IAdsService CreateAdsService()
        {
            var adsService = _builder.QuickBuild();

            _adsService = adsService;
            _adsService.Run();

            return _adsService;
        }

        public IMediationNetworkAnalytics CreateAnalytics() => _creator.Instantiate<LevelPlaySdkAnalytics>(_adsService);
    }
}