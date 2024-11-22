using System;
using com.unity3d.mediation;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class LevelPlayInterAd : AdUnitLogic
    {
        private readonly IAdUnitKey _key;
        private LevelPlayInterstitialAd _levelPlayInterstitial;

        public LevelPlayInterAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : base(key,
            GetInterEvent(out LevelPlayInterstitialAd interstitial, key), coroutineRunner)
        {
            _key = key;
            _levelPlayInterstitial = interstitial;
        }

        private static LevelPlaySdkInterEvents GetInterEvent(out LevelPlayInterstitialAd interstitial, IAdUnitKey key)
        {
            interstitial = new LevelPlayInterstitialAd(key.StringValue);
            
            return new LevelPlaySdkInterEvents(interstitial);
        }
        
        protected override bool IsAdReady() => _levelPlayInterstitial.IsAdReady();

        protected override void ShowAd() => _levelPlayInterstitial.ShowAd();
        public override void Load() => _levelPlayInterstitial.LoadAd();
    }
}