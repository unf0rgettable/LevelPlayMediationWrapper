using com.unity3d.mediation;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlayRewardAd : AdUnitLogic
    {
        private readonly IAdUnitKey _key;
        private LevelPlayRewardedAd _levelPlayRewardedAd;

        public LevelPlayRewardAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : base(key,
            GetInterEvent(out LevelPlayRewardedAd rewarded, key), coroutineRunner)
        {
            _key = key;
            _levelPlayRewardedAd = rewarded;
        }

        private static LevelPlaySdkRewardEvents GetInterEvent(out LevelPlayRewardedAd rewarded, IAdUnitKey key)
        {
            rewarded = new LevelPlayRewardedAd(key.StringValue);
            
            return new LevelPlaySdkRewardEvents(rewarded);
        }
        
        protected override bool IsAdReady() => _levelPlayRewardedAd.IsAdReady();

        protected override void ShowAd() => _levelPlayRewardedAd.ShowAd();
        public override void Load() => _levelPlayRewardedAd.LoadAd();
    }
}