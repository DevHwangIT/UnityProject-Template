using UnityEngine;

namespace Libraray.Ads
{/*
    /// <summary>
    /// 전면 광고
    /// 전면 광고는 1시간 후에 다시 광고를 볼 수 있습니다.
    /// </summary>
    public class AdsInterstitial
    {
        string _adUnitId = "";
        float _interstitialAdTimeout = 5.0f;//3600f;
        float _interstitialtime = 0.0f;
        InterstitialAd _interstitialAd;

        public AdsInterstitial(string adUnitId)
        {
            _adUnitId = adUnitId;
            _interstitialtime = Time.time;
        }

        /// <summary>
        /// 전면광고 로드하기
        /// </summary>
        void LoadInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

            // send the request to load the ad.
            InterstitialAd.Load(_adUnitId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;

                    RegisterEventHandlers(_interstitialAd);
                });
        }


        /// <summary>
        /// 전면 광고 보여주기
        /// </summary>
        public void ShowInterstitialAd()
        {
            // 1시간이 지나면 광고를 보여준다.
            if (Time.time - _interstitialtime > _interstitialAdTimeout)
            {
                _interstitialtime = Time.time;
                Debug.Log("Interstitial ad is not ready yet.");
                return;
            }

            LoadInterstitialAd();

            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }


        /// <summary>
        ///  이벤트 핸들러 등록
        /// </summary>
        /// <param name="interstitialAd"></param>
        void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            // Raised when the ad is estimated to have earned money.
            interstitialAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));

                      _interstitialAd.Destroy();
            };
            // Raised when an impression is recorded for an ad.
            interstitialAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            interstitialAd.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial ad full screen content closed.");
                _interstitialAd.Destroy();
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);

                _interstitialAd.Destroy(); 
            };
        }
    }*/
}