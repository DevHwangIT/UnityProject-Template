using UnityEngine;


namespace Libraray.Ads
{
/*
    /// <summary>
    /// 보상형 광고
    /// 보상형 광고는 1시간 후에 다시 보상을 받을 수 있습니다.
    /// </summary>
    public class AdsReward
    {
        // 보상형 광고 객체
        RewardedAd _rewardedAd;
        string _adUnitId;

        float _rewardedAdTimeout = 5.0f;//3600f;
        float _rewarditime = 0.0f;


        public AdsReward(string adUnitId)
        {
            _adUnitId = adUnitId;
             _rewarditime = Time.time;
        }

        /// <summary>
        /// 보상형 광고 로드하기
        /// </summary>
        void LoadRewardedAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            Debug.Log("Loading the rewarded ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

            // send the request to load the ad.
            RewardedAd.Load(_adUnitId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedAd = ad;

                    // 이벤트 핸들러 등록
                    RegisterEventHandlers(_rewardedAd);

                });
        }

        /// <summary>
        /// 보상형 광고 보여주기
        /// 이곳에서 유저에게 보상을 주는 코드를 작성하면 됩니다.
        /// </summary>
        public void ShowRewardedAd()
        {
            // 1시간이 지났는지 체크
            if (Time.time - _rewarditime > _rewardedAdTimeout)
            {
                _rewarditime = Time.time;
                Debug.Log("The rewarded ad is not ready to be shown.");
                return;
            }


            LoadRewardedAd();

            const string rewardMsg =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    // TODO: 유저에게 보상 주는 코드 작성하기
                    GiveReward();
                    Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
                });
            }
        }

        void GiveReward()
        {
            // 보상을 주는 코드 작성
        }


        /// <summary>
        /// 이벤트 핸들러 등록
        /// </summary>
        /// <param name="ad"></param>
        void RegisterEventHandlers(RewardedAd ad)
        {
            // 광고가 수익을 발생시켰을 때
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(string.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));

                // 광고 닫기
                _rewardedAd.Destroy();
            };

            // 광고 노출되었을 때
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };

            // 광고 클릭했을 때
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // 광고가 전체화면 콘텐츠를 열었을 때
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
            };
            // 광고가 전체화면 콘텐츠를 닫았을 때
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded ad full screen content closed.");


                // 광고 닫기
                _rewardedAd.Destroy();
            };
            // 광고가 전체화면 콘텐츠 호출에 실패했을 때
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);

                // 광고 닫기
                _rewardedAd.Destroy();
            };
        }
    }*/
}