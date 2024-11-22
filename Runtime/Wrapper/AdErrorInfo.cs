using com.unity3d.mediation;
using LittleBitGames.Ads.AdUnits;
using UnityEngine;

namespace LevelPlayMediationWrapper.Runtime.Wrapper
{
    public class AdErrorInfo : IAdErrorInfo
    {
        public AdErrorInfo(LevelPlayAdError adFailureEventArgs)
        {
            Message = adFailureEventArgs.ErrorMessage;
            MediatedNetworkErrorCode = adFailureEventArgs.ErrorCode;
            MediatedNetworkErrorMessage = "Undefined";
            
            Debug.LogError("adFailureEventArgs.Message " + Message);
        }

        public string Message { get; }
        public int MediatedNetworkErrorCode { get; }
        public string MediatedNetworkErrorMessage { get; }
    }
}