using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(CanvasGroup))]
public class UIWindow : MonoBehaviour, IEventSystemHandler, ISelectHandler, IPointerDownHandler
{
	protected static UIWindow _FucusedWindow;
	public static UIWindow FocusedWindow => _FucusedWindow;
	[SerializeField] private UIWindowID _WindowId = UIWindowID.None;
	protected bool _IsFocused = false;
	private CanvasGroup _CanvasGroup;

	[SerializeField] private bool isNotHideWithCancelInput;
	public bool NotHideWithCancelInput => isNotHideWithCancelInput;
	
	public UIWindowID ID
	{
		get { return this._WindowId; }
		set { this._WindowId = value; }
	}

	public bool IsVisible
	{
		get { return (this._CanvasGroup != null && this._CanvasGroup.alpha > 0f) ? true : false; }
	}

	public bool IsFocused
	{
		get { return this._IsFocused; }
	}

	protected virtual void Awake()
	{
		this._CanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
		this.transform.SetParent(UIWindowManager.Instance.transform);
		UIWindowManager.Instance.SortingWindowUIOrder();
	}

	protected virtual bool IsActive()
	{
		return (this.enabled && this.gameObject.activeInHierarchy);
	}

	public virtual void OnSelect(BaseEventData eventData)
	{
		this.Focus();
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		this.Focus();
	}

	public virtual void Focus()
	{
		if (this._IsFocused)
			return;

		this._IsFocused = true;

		UIWindow.OnBeforeFocusWindow(this);
	}

	public virtual void Show()
	{
		if (!this.IsActive())
			return;

		_CanvasGroup.alpha = 1;
		_CanvasGroup.blocksRaycasts = true;
		this.Focus();
	}

	public virtual void Hide()
	{
		if (!this.IsActive())
			return;

		_CanvasGroup.alpha = 0;
		_CanvasGroup.blocksRaycasts = false;
	}

	#region Static Methods

	public static List<UIWindow> GetWindows()
	{
		List<UIWindow> windows = new List<UIWindow>();
		UIWindow[] ws = FindObjectsOfType<UIWindow>();

		foreach (UIWindow w in ws)
		{
			if (w.gameObject.activeInHierarchy)
				windows.Add(w);
		}

		return windows;
	}

	public static UIWindow GetWindow(UIWindowID id)
	{
		foreach (UIWindow window in UIWindow.GetWindows())
			if (window.ID == id)
				return window;
		return null;
	}

	public static void FocusWindow(UIWindowID id)
	{
		if (UIWindow.GetWindow(id) != null)
			UIWindow.GetWindow(id).Focus();
	}

	protected static void OnBeforeFocusWindow(UIWindow window)
	{
		if (_FucusedWindow != null)
			_FucusedWindow._IsFocused = false;

		_FucusedWindow = window;
	}

	#endregion
}