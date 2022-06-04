using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class ModalBoxUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    protected CanvasGroup GetCanvasGroup
    {
        get
        {
            if (canvasGroup == null)
                canvasGroup = this.GetComponent<CanvasGroup>();
            return canvasGroup;
        }
    }
    
    protected void SetParent()
    {
        transform.SetParent(UIModalBoxManager.Instance.transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    public bool IsActive { get { return (this.enabled && this.gameObject.activeInHierarchy); } }
    
    protected void Awake()
    {
        SetParent();
    }

    public virtual void Initialize()
    {
        SetParent();
    }
    
    public void Show()
    {
        GetCanvasGroup.alpha = 1;
        GetCanvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        GetCanvasGroup.alpha = 0;
        GetCanvasGroup.blocksRaycasts = false;
    }

    public void SetSize(float width, float height)
    {
        this.transform.localPosition = new Vector3(width, height, 0);
    }

    private static List<ModalBoxUI> GetActiveModalBoxes()
    {
        List<ModalBoxUI> modalBoxes = new List<ModalBoxUI>();
        modalBoxes.AddRange(FindObjectsOfType<ModalBoxUI>());
        return modalBoxes;
    }

    public static T GetModalBox<T>() where T : ModalBoxUI
    {
        foreach (var modalBoxUI in GetActiveModalBoxes())
            if (modalBoxUI.GetType() == typeof(T))
                return (T) modalBoxUI;
        return null;
    }
    
    public static List<T> GetModalBoxes<T>() where T : ModalBoxUI
    {
        List<T> modalBoxesUI = new List<T>();
        foreach (var modalBoxUI in GetActiveModalBoxes())
        {
            if (modalBoxUI.GetType() == typeof(T))
                modalBoxesUI.Add((T) modalBoxUI);
        }
        return modalBoxesUI;
    }

    public static T GetNotActiveModalBoxInHierarchyView<T>() where T : ModalBoxUI
    {
        foreach (ModalBoxUI mBox in ModalBoxUI.GetActiveModalBoxes())
            if (mBox.IsActive == false && mBox.GetType() == typeof(T))
                return (T) mBox;
        return null;
    }
    
    public static ModalBoxUI GetNotActiveModalBoxInHierarchyView(ModalBoxUI type)
    {
        foreach (ModalBoxUI mBox in ModalBoxUI.GetActiveModalBoxes())
            if (mBox.IsActive == false && mBox.GetType() == type.GetType())
                return mBox;
        return null;
    }
}
