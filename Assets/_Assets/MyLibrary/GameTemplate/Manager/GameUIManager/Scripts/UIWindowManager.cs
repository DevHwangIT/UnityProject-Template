using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIWindowManager : MonoBehaviour
{
	#region Singleton

	private static UIWindowManager _instance;

	public static UIWindowManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (UIWindowManager) FindObjectOfType(typeof(UIWindowManager));
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
					GameObject parentObj = new GameObject("Windows");
					parentObj.transform.SetParent(parentCanvas.transform);
					_instance = parentObj.AddComponent<UIWindowManager>();
				}
			}
			return _instance;
		}
	}

	#endregion

	[SerializeField] private string m_EscapeInputName = "Cancel";
	public string escapeInputName
	{
		get { return this.m_EscapeInputName; }
	}

	private void Awake()
	{
		SortingWindowUIOrder();
	}

	protected virtual void Update()
	{
		if (Input.GetButtonDown(this.m_EscapeInputName)) 
		{
			List<UIWindow> windows = UIWindow.GetWindows();
			UIWindow lastWindow = null;
			foreach (UIWindow window in windows)
			{
				if (!window.IsVisible || window.NotHideWithCancelInput)
					continue;
					
				if (lastWindow == null)
					lastWindow = window;
				else
				{
					if (lastWindow.ID.Priority < window.ID.Priority)
						lastWindow = window;
					else
					{
						Debug.Log(window.ID.ToString());
					}
				}
			}

			if (lastWindow == null)
			{
				UIWindow.GetWindow(UIWindowID.GameMenu).Show();
			}
			else
			{
				if (lastWindow.IsVisible)
					lastWindow.Hide();
			}
		}
	}

	public void SortingWindowUIOrder()
	{
		List<UIWindow> windows = UIWindow.GetWindows();
		windows = windows.OrderBy(x => x.ID).ToList();
		foreach (var window in windows)
		{
			window.transform.SetAsLastSibling();
		}
	}
}