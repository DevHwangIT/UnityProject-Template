using UnityEngine;

namespace Libraray.Ads
{/*
    // 배너 광고
    public class AdsBanner
    {
        int _bannerWidth = 320;
        int _bannerHeight = 50;
        string _adUnitId;

        // 배너 객체
        BannerView _bannerView;


        public AdsBanner(string AdsUnitid, int Width, int Height)
        {
            _adUnitId = AdsUnitid;
            _bannerWidth = Width;
            _bannerHeight = Height;
        }

        /// <summary>
        /// 지정한 크기만큼의 배너광고 화면 상단에 생성
        /// </summary>
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // 배너가 이미 있으면 삭제
            if (_bannerView != null)
            {
                DestroyAd();
            }

            // 지정한 크기만큼의 배너 생성
            //_bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);
            _bannerView = new BannerView(_adUnitId, new AdSize(_bannerWidth, _bannerHeight), AdPosition.Top);

            ListenToAdEvents();
        }

        /// <summary>
        /// Destroys the banner view.
        /// </summary>
        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }

        /// <summary>
        /// 배너 광고 켜기
        /// </summary>
        public void LoadAd()
        {
            // create an instance of a banner view first.
            if (_bannerView == null)
            {
                CreateBannerView();
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
        }

        /// <summary>
        /// listen to events the banner view may raise.
        /// </summary>
        private void ListenToAdEvents()
        {
            // Raised when an ad is loaded into the banner view.
            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                    + _bannerView.GetResponseInfo());
            };
            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);
            };
            // Raised when the ad is estimated to have earned money.
            _bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(string.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            _bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            _bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            _bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
        }
    }*/
}