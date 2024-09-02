using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace AdPlatform
{
    /// <summary>
    /// 배너 광고 게임오브젝트 스크립트
    /// 로컬 이미지로 광고를 표시하고 클릭시 다운로드 링크로 이동한다.
    /// 추후 서버에서 광고 이미지와 링크를 받아와서 표시할 수 있도록 수정할 수 있다.
    /// </summary>
    public class BannerAdBehaviour : MonoBehaviour
    {
        [System.Serializable]
        public class BannerAd
        {
            public string adName;
            public Sprite adImage;

            [Multiline(5)]
            public string downloadLink;

            [Tooltip("다음 광고 나오기까지 시간(정수 : 초)")]
            public float displayDuration;
        }

        [Header("Ads Data")]
        [SerializeField] List<BannerAd> bannerAds = new List<BannerAd>();

        [Header("UI - Banner Ads")]
        [SerializeField] Image bannerImage;
        [SerializeField] Button bannerButton;
        [SerializeField] RectTransform bannerObject;

        int currentAdIndex = 0;         // 현재 광고 인덱스
        Coroutine rotationCoroutine;

        private void Start()
        {
            if (bannerAds.Count > 0)
            {
                bannerButton.onClick.AddListener(OnBannerClicked);
                StartBannerRotation();
            }
            else
            {
                Debug.LogWarning("No banner ads available.");
                bannerObject.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 배너광고 추가 함수
        /// 인스펙터창에서도 추가할 수 있다.
        /// </summary>
        /// <param name="name"> 광고 이름 </param>
        /// <param name="image"> 광고 이미지 스프라이트 </param>
        /// <param name="clickUrl"> 다운로드 링크 url </param>
        /// <param name="duration"> 광고가 표시되는 시간(디폴트 : 5초) </param>
        public void AddBannerAd(string name, Sprite image, string clickUrl, float duration = 5f)
        {
            bannerAds.Add(new BannerAd
            {
                adName = name,
                adImage = image,
                downloadLink = clickUrl,
                displayDuration = duration
            });

            if (bannerAds.Count == 1)
            {
                bannerButton.onClick.AddListener(OnBannerClicked);
                StartBannerRotation();
                bannerObject.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 광고 제거
        /// </summary>
        /// <param name="name"> 삭제할 광고 이름 </param>
        public void RemoveBannerAd(string name)
        {
            bannerAds.RemoveAll(ad => ad.adName == name);

            if (bannerAds.Count == 0)
            {
                StopBannerRotation();
                bannerObject.gameObject.SetActive(false);
            }
            else
            {
                currentAdIndex = 0;
                StartBannerRotation();
            }
        }

        /// <summary>
        /// 배너 위치 설정 함수
        /// </summary>
        /// <param name="edge"> 위 아래 좌 우</param>
        /// <param name="offset"> 모서리에서 떨어질 간격 좌표</param>
        public void SetBannerPosition(RectTransform.Edge edge, Vector2 offset)
        {
            bannerObject.SetInsetAndSizeFromParentEdge(edge, offset.x, bannerObject.rect.width);
            bannerObject.SetInsetAndSizeFromParentEdge(
                edge == RectTransform.Edge.Left || edge == RectTransform.Edge.Right ? RectTransform.Edge.Top : RectTransform.Edge.Left,
                offset.y, bannerObject.rect.height);
        }

        #region private Banner Ads Functions

        /// <summary>
        /// 배너광고 시작
        /// 여러 광고가 순서대로 돌아간다.
        /// </summary>
        void StartBannerRotation()
        {
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }
            rotationCoroutine = StartCoroutine(RotateBanners());
        }

        /// <summary>
        /// 배너광고 정지
        /// </summary>
        void StopBannerRotation()
        {
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
                rotationCoroutine = null;
            }
        }

        /// <summary>
        /// 광고 순회 코루틴
        /// </summary>
        /// <returns></returns>
        IEnumerator RotateBanners()
        {
            while (true)
            {
                DisplayBanner(bannerAds[currentAdIndex]);
                yield return new WaitForSeconds(bannerAds[currentAdIndex].displayDuration);

                // 다음 광고
                currentAdIndex = (currentAdIndex + 1) % bannerAds.Count;
            }
        }

        /// <summary>
        /// 배너광고 표시
        /// </summary>
        /// <param name="ad"></param>
        void DisplayBanner(BannerAd ad)
        {
            bannerImage.sprite = ad.adImage;

        }

        /// <summary>
        /// 배너광고 클릭
        /// </summary>
        void OnBannerClicked()
        {
            if (bannerAds.Count > 0)
            {
                string url = bannerAds[currentAdIndex].downloadLink;
                Application.OpenURL(url);
            }
        }
        #endregion private Banner Ads Functions
    }
}