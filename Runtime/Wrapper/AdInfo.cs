using com.unity3d.mediation;
using LittleBitGames.Ads.AdUnits;
using UnityEngine;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class AdInfo : IAdInfo
    {
        public AdInfo(LevelPlayAdInfo impressionData)
        {
            AdUnitIdentifier = impressionData.AdUnitId;
            AdFormat = impressionData.AdFormat;
            
            if (impressionData.Revenue != null)
            {
                Revenue = (double)impressionData.Revenue;
            }
            else
            {
                Revenue = 0;
            }
            
            RevenuePrecision = impressionData.Precision;
            NetworkName = impressionData.AdNetwork;
            NetworkPlacement = impressionData.PlacementName;
            Placement = "Undefined";
            CreativeIdentifier = "Undefined";
            DspName = "Undefined";
        }
        
        public string AdUnitIdentifier { get; }
        public string AdFormat { get; }
        public string NetworkName { get; }
        public string NetworkPlacement { get; }
        public string Placement { get; }
        public string CreativeIdentifier { get; }
        public double Revenue { get; }
        public string RevenuePrecision { get; }
        public string DspName { get; }
    }
}