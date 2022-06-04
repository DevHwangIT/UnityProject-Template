using UnityEditor;
using UnityEngine;

namespace MyLibrary.Tools
{
    public interface IDebugSystemGUI
    {
        void OnDrawGUI(DebugSystemDataScriptable systemData, DebugSystemGUIDataScriptable guiData);
    }
    
    public class DebugSystemGUI : MonoBehaviour
    {
        #region EnumType
        [System.Serializable]
        enum DebugViewType
        {
            SystemInfo,
            DebugLog,
            CheatCode
        }
        #endregion
        
        [Header("Scriptable Object")]
        [SerializeField] private DebugSystemDataScriptable _debugSystemData;
        [SerializeField] private DebugSystemGUIDataScriptable _debugSystemGUIData;
        
        //GUI Viwer
        private DebugSystemGUICheatViewer _cheatGUIViewer;
        private DebugSystemGUILogViewer _logGUIViewer;    
        private DebugSystemGUISystemViewer _systemGUIViewer;
        
        private DebugViewType viewType = DebugViewType.SystemInfo;
        private bool isLogIn = false;
        private bool isActive = false;
        private string _inputPw = "";

        #region InitializeFunc
        public void Initialize()
        {
            if (_cheatGUIViewer == null)
            {
                if (this.GetComponent<DebugSystemGUICheatViewer>() != null)
                    _cheatGUIViewer = this.GetComponent<DebugSystemGUICheatViewer>();
                else
                    _cheatGUIViewer = gameObject.AddComponent<DebugSystemGUICheatViewer>();
                _cheatGUIViewer.hideFlags = HideFlags.HideInInspector;
            }

            if (_logGUIViewer == null)
            {
                if (this.GetComponent<DebugSystemGUILogViewer>() != null)
                    _logGUIViewer = this.GetComponent<DebugSystemGUILogViewer>();
                else
                    _logGUIViewer = gameObject.AddComponent<DebugSystemGUILogViewer>();
                _logGUIViewer.hideFlags = HideFlags.HideInInspector;
            }
            
            if (_systemGUIViewer == null)
            {
                if (this.GetComponent<DebugSystemGUISystemViewer>() != null)
                    _systemGUIViewer = this.GetComponent<DebugSystemGUISystemViewer>();
                else
                    _systemGUIViewer = gameObject.AddComponent<DebugSystemGUISystemViewer>();
                _systemGUIViewer.hideFlags = HideFlags.HideInInspector;
            }

            if (_debugSystemData == null || _debugSystemGUIData == null)
            {
                Debug.LogError("Null Exception in Script!! Please Check This Problem with Scriptable Asset Path");
            }
            
            UnityEngine.Debug.Log("Debug System Initialized");
        }
        #endregion
        
        private void Awake()
        {
            Initialize();
        }

        private void OnGUI()
        {
            GUI.skin = _debugSystemGUIData.debugSystemGUISkin;
            GUI.skin.label.CalcScreenSize(Vector2.one * _debugSystemData.scaleFactor);
            
            if (_debugSystemData.getisDebugActive)
            {
                isActive = GUILayout.Toggle (isActive, "Debugging");
                GUILayout.Space(5);
                if (isActive)
                {
                    SetInit();
                    if (isLogIn)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button(_debugSystemGUIData.imagesResources.systemInfoImage, GUILayout.Width(_debugSystemGUIData.IconSize.x), GUILayout.Height(_debugSystemGUIData.IconSize.y)))
                                    viewType = DebugViewType.SystemInfo;
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.Space(1);
                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button(_debugSystemGUIData.imagesResources.clearImage, GUILayout.Width(_debugSystemGUIData.IconSize.x), GUILayout.Height(_debugSystemGUIData.IconSize.y)))
                                    viewType = DebugViewType.DebugLog;
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.Space(1);
                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button(_debugSystemGUIData.imagesResources.infoImage, GUILayout.Width(_debugSystemGUIData.IconSize.x), GUILayout.Height(_debugSystemGUIData.IconSize.y)))
                                    viewType = DebugViewType.CheatCode;
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndHorizontal();

                        DrawSelectView();
                    }
                    else
                    {
                        GUILayout.Label("Debug Mode Password");
                        GUILayout.Space(5);
                        _inputPw = GUILayout.TextField(_inputPw, GUILayout.Width(Screen.width),
                            GUILayout.Height(Screen.height * 0.1f));
                        GUILayout.Space(5);
                        if (GUILayout.Button("LogIn", GUILayout.Width(Screen.width), GUILayout.Height(Screen.height * 0.1f)))
                        {
                            isLogIn = _debugSystemData.ComparePwKey(_inputPw);
                        }
                    }
                }
            }
        }

        private void SetInit()
        {
            _debugSystemData.scaleFactor = GUILayout.HorizontalSlider(_debugSystemData.scaleFactor, 0, 2);
            GUILayout.Space(20);

            foreach (GUIStyle style in GUI.skin)
            {
                style.fontSize = (int) (_debugSystemGUIData.FontSize * _debugSystemData.scaleFactor);
            }
        }


        private void DrawSelectView()
        {
            switch (viewType)
            {
                case DebugViewType.SystemInfo:
                    _systemGUIViewer.OnDrawGUI(_debugSystemData, _debugSystemGUIData);
                    break;

                case DebugViewType.DebugLog:
                    _logGUIViewer.OnDrawGUI(_debugSystemData, _debugSystemGUIData);
                    break;

                case DebugViewType.CheatCode:
                    _cheatGUIViewer.OnDrawGUI(_debugSystemData, _debugSystemGUIData);
                    break;
            }
        }
    }
}
