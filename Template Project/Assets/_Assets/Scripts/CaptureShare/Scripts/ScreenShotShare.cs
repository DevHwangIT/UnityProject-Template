using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;

namespace Share
{
    /// <summary>
    /// �ΰ��� ��ũ���� ���� ���
    /// </summary>
    public class ScreenshotShare : MonoBehaviour
    {
        private string screenshotName;

        /// <summary>
        ///  ��ư�� ������ ��ũ������ ��� ����.
        ///  ��ư �̺�Ʈ�� �����ϱ�
        /// </summary>
        public void CaptureAndShare()
        {
            StartCoroutine(CaptureAndShareScreenshot());
        }

        /// <summary>
        /// ĸó�ϱ�
        /// ��ư �̺�Ʈ�� �����ϱ�
        /// </summary>
        public void Capture()
        {
            StartCoroutine(CaptureScreenshot());
        }

        #region capture screenshot

        /// <summary>
        /// ��ũ���� ��� �ٷ� ����
        /// </summary>
        /// <returns></returns>
        IEnumerator CaptureAndShareScreenshot()
        {
            yield return new WaitForEndOfFrame();

            Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();

            byte[] bytes = screenshot.EncodeToPNG();
            string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            screenshotName = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllBytes(screenshotName, bytes);


            ShareScreenshot();
        }

        /// <summary>
        /// ��ũ������ ���
        /// </summary>
        /// <returns></returns>
        IEnumerator CaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();

            Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();

            byte[] bytes = screenshot.EncodeToPNG();
            string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            screenshotName = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllBytes(screenshotName, bytes);
        }


        /// <summary>
        /// ����Ƽ ������ : ��ũ���� ���
        /// �ȵ���̵�, IOS : ��ũ���� ���� �� ����
        /// </summary>
#if UNITY_EDITOR
        private void ShareScreenshot()
        {
            string path = SaveScreenshotInEditor();
            UnityEditor.EditorUtility.DisplayDialog("Share Screenshot",
                "Screenshot saved at: " + path + "\nIt would be shared on an Android device.", "OK");
        }

        /// <summary>
        /// �����Ϳ� ��ũ����
        /// </summary>
        /// <returns></returns>
        string SaveScreenshotInEditor()
        {
            // ��ũ���� ���� ��� ����
            string folderPath = Path.Combine(Application.dataPath, "../Screenshots");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = "Screenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            string fullPath = Path.Combine(folderPath, fileName);

            // ���� ���� ��ũ���� ĸó
            int resWidth = Screen.width;
            int resHeight = Screen.height;

            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            Camera.main.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            Camera.main.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            Camera.main.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            // ��ũ������ ���Ϸ� ����
            byte[] bytes = screenShot.EncodeToPNG();
            File.WriteAllBytes(fullPath, bytes);
            Destroy(screenShot);

            Debug.Log("Screenshot saved to: " + fullPath);

            // ���� �����ͺ��̽� �������� (Unity �����Ϳ��� ������ �ٷ� �� �� �ֵ���)
            AssetDatabase.Refresh();

            return fullPath;
        }

#elif UNITY_ANDROID || UNITY_IOS
    private void ShareScreenshot()
    {
        string shareText = "Check out my game screenshot!";
        string shareUrl = "file://" + screenshotName;

#if UNITY_ANDROID
        StartCoroutine(ShareAndroidScreenshot(shareText, shareUrl));
#elif UNITY_IOS
    CallSocialShareAdvanced(shareText, shareUrl, "");
#else
    Debug.Log("No sharing set up for this platform.");
#endif
    }
#endif

#if UNITY_ANDROID
        /// <summary>
        /// �ȵ���̵�� ��ũ����
        /// </summary>
        /// <param name="shareText"></param>
        /// <param name="shareUrl"></param>
        /// <returns></returns>
        private IEnumerator ShareAndroidScreenshot(string shareText, string shareUrl)
        {
            yield return new WaitForEndOfFrame();

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", "android.intent.action.SEND");
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", "android.intent.extra.TEXT", shareText);

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", shareUrl);
            intentObject.Call<AndroidJavaObject>("putExtra", "android.intent.extra.STREAM", uriObject);

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Screenshot");

            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                currentActivity.Call("startActivity", jChooser);
            }));
        }
#endif

        /// <summary>
        /// IOS �� ����. ���� �����ϱ�
        /// </summary>
#if UNITY_IOS
    public struct ConfigStruct
    {
        public string title;
        public string message;
        public string url;
    }

    [DllImport ("__Internal")] private static extern void showSocialShareUI(ConfigStruct conf);

    public static void CallSocialShareAdvanced(string title, string message, string url)
    {
        ConfigStruct conf = new ConfigStruct();
        conf.title = title;
        conf.message = message;
        conf.url = url;
        showSocialShareUI(conf);
    }
#endif
    }

    #endregion capture screenshot
}