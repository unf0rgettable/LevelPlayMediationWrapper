using com.unity3d.mediation;
using LevelPlayMediationWrapper.Runtime.Wrapper;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;

namespace Wrapper
{
    public class LevelPlayBannerAd: AdUnitLogic
    {
        private readonly IAdUnitKey _key;
        private com.unity3d.mediation.LevelPlayBannerAd _levelPlaybannerAd;

        public LevelPlayBannerAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : base(key,
            GetInterEvent(out com.unity3d.mediation.LevelPlayBannerAd banner, key), coroutineRunner)
        {
            _key = key;
            _levelPlaybannerAd = banner;
        }

        private static LevelPlaySDKBannerEvents GetInterEvent(out com.unity3d.mediation.LevelPlayBannerAd banner, IAdUnitKey key)
        {
            banner = new com.unity3d.mediation.LevelPlayBannerAd(key.StringValue, LevelPlayAdSize.BANNER,
                LevelPlayBannerPosition.BottomCenter, "banner", false);
            
            banner.ResumeAutoRefresh();
            
            return new LevelPlaySDKBannerEvents(banner);
        }
        
        protected override bool IsAdReady() => true;

        protected override void ShowAd() => _levelPlaybannerAd.ShowAd();
        public override void Load() => _levelPlaybannerAd.LoadAd();
    }
}