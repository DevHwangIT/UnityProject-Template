using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

namespace AdPlatform
{
    /// <summary>
    /// 팝업 광고 스크립트
    /// 로컬 비디오 광고를 표시하고 다운로드 링크로 이동하는 기능을 제공
    /// 추후 서버에서 광고 데이터를 받아와서 표시할 수 있도록 확장할 수 있다.
    /// </summary>
    public class PopUpAdBehaviour : MonoBehaviour
    {
        [System.Serializable]
        public class AdItem
        {
            public string adName;
            public float probability;
            public Sprite adImage;

            [Tooltip("StreamingAssets 경로에 영상을 넣는다.")]
            public string videoPath;

            [Multiline(5)]
            public string downloadLink;
        }


        [Header("Ads Data")]
        [SerializeField] List<AdItem> adItems = new List<AdItem>();

        [Header("UI - Ads Image")]
        [SerializeField] GameObject adPanel;
        [SerializeField] Image adDisplayImage;
        [SerializeField] Button downloadButton;

        [Header("UI - Ads Video")]
        [SerializeField] GameObject videoPanel;
        [SerializeField] RawImage videoDisplayImage;
        [SerializeField] VideoPlayer videoPlayer;
        [SerializeField] RenderTexture videoRenderTexture;

        [Header("Debug Help")]
        [SerializeField] TextMeshProUGUI videoNoticeText; // 런타임시 비활성화
        [SerializeField] TextMeshProUGUI ImageNoticeText; // 런타임시 비활성화

        private void Start()
        {
            downloadButton.onClick.AddListener(OnAdClicked);

            // 에디터에서만 보이고 런타임시 비활성화
            videoNoticeText.enabled = false;
            ImageNoticeText.enabled = false;
        }

        public void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowRandomAd();
            }
#endif
        }

        /// <summary>
        /// 랜덤 광고 보여주기 기능
        /// 버튼 이벤트, 또는 인게임 컨텐츠와 연결해서 사용하기
        /// </summary>
        public void ShowRandomAd()
        {
            float totalProbability = 0f;
            foreach (var ad in adItems)
            {
                totalProbability += ad.probability;
            }
            float randomValue = Random.Range(0f, totalProbability);
            float currentProbability = 0f;
            foreach (var ad in adItems)
            {
                currentProbability += ad.probability;
                if (randomValue <= currentProbability)
                {
                    DisplayAd(ad);
                    break;
                }
            }
        }

        // 런타임 중 광고 추가할 때 사용
        public void AddAd(string name, float probability, Sprite image, string videoPath, string downloadLink)
        {
            adItems.Add(new AdItem
            {
                adName = name,
                probability = probability,
                adImage = image,
                videoPath = videoPath,
                downloadLink = downloadLink
            });
        }

        // 런타임 중 광고 확률 조정
        public void SetAdProbability(string adName, float newProbability)
        {
            AdItem ad = adItems.Find(item => item.adName == adName);
            if (ad != null)
            {
                ad.probability = newProbability;
            }
        }

        #region Private Ads Functions

        void DisplayAd(AdItem ad)
        {
            adDisplayImage.sprite = ad.adImage;
            adPanel.SetActive(true);
            // 비디오 광고인 경우 비디오 플레이어를 실행할 수 있습니다.
            if (!string.IsNullOrEmpty(ad.videoPath))
            {
                StartCoroutine(PlayVideo(ad.videoPath));
            }
        }

        IEnumerator PlayVideo(string videoPath)
        {
            // 비디오 패널 활성화
            videoPanel.SetActive(true);

            // VideoPlayer 설정
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoPath);
            videoPlayer.targetTexture = videoRenderTexture;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.playOnAwake = false;

            // 비디오 준비
            videoPlayer.Prepare();

            // 비디오가 준비될 때까지 대기
            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            // RawImage에 RenderTexture 할당
            videoDisplayImage.texture = videoRenderTexture;

            // 비디오 재생
            videoPlayer.Play();

            // 비디오 재생이 끝날 때까지 대기
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            // 재생 완료 후 비디오 패널 비활성화
            videoPanel.SetActive(false);
            videoDisplayImage.texture = null;

            yield return null;
        }

        void OnAdClicked()
        {
            AdItem currentAd = GetCurrentAd();
            if (currentAd != null)
            {
                Application.OpenURL(currentAd.downloadLink);
            }
        }

        AdItem GetCurrentAd()
        {
            // 현재 표시 중인 광고를 찾아 반환합니다.
            return adItems.Find(ad => ad.adImage == adDisplayImage.sprite);
        }
        #endregion Private Ads Functions


    }
}