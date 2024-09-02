using UnityEngine;

namespace Libraray.Ads
{/*
    /// <summary>
    /// 광고 컨트롤러
    /// 보상형, 전면형, 배너형 광고 등을 켜고 끄는 역할을 한다.
    /// </summary>
    public class AdsController : Singleton<AdsController>
    {
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111"; // 테스트용
#elif UNITY_IPHONE
        private string _adUnitId = "ca-app-pub-3940256099942544/2934735716"; // 테스트용
#else
        private string _adUnitId = "unused";
#endif

        AdsBanner _adsBanner = null;
        AdsReward _adsReward = null;
        AdsInterstitial _adsInterstitial = null;

        private void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(initStatus => { });

            _adsBanner = new AdsBanner(_adUnitId, 320, 50);
            _adsReward = new AdsReward(_adUnitId);
            _adsInterstitial = new AdsInterstitial(_adUnitId);
        }

        /// <summary>
        /// 배너 광고 켜기 래핑
        /// </summary>
        public void ShowBanner()
        {
            _adsBanner.LoadAd();
        }

        /// <summary>
        /// 배너 광고 끄기 래핑
        /// </summary>
        public void HideBanner()
        {
            _adsBanner.DestroyAd();
        }


        /// <summary>
        /// 보상형 광고 켜기 래핑
        /// </summary>
        public void ShowRewardedAd()
        {
            _adsReward.ShowRewardedAd();
        }

        /// <summary>
        ///  전면형 광고 켜기 래핑
        /// </summary>
        public void ShowInterstitialAd()
        {
            _adsInterstitial.ShowInterstitialAd();
        }

    }*/
}