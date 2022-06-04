using UnityEngine;
using UnityEngine.UI;

public class UIModalBoxManager : MonoBehaviour
{
    #region Singleton
    private static UIModalBoxManager _instance;
    public static UIModalBoxManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (UIModalBoxManager) FindObjectOfType(typeof(UIModalBoxManager));
                if (_instance == null)
                {
                    Canvas parentCanvas = FindObjectOfType<Canvas>();
                    if (parentCanvas == null)
                    {
                        GameObject canvasGameObj = new GameObject("Canvas (UI)");
                        parentCanvas = canvasGameObj.AddComponent<Canvas>();
                        canvasGameObj.AddComponent<CanvasScaler>();
                        canvasGameObj.AddComponent<GraphicRaycaster>();
                        parentCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    }
                    GameObject parentObj = new GameObject("Modal Boxes");
                    parentObj.transform.SetParent(parentCanvas.transform);
                    _instance = parentObj.AddComponent<UIModalBoxManager>();
                }
            }
            return _instance;
        }
    }
    #endregion
}
