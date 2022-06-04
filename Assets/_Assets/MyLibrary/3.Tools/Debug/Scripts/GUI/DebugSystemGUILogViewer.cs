using UnityEngine;

namespace MyLibrary.Tools
{
    public class DebugSystemGUILogViewer : MonoBehaviour, IDebugSystemGUI
    {
        Vector2 scrollPosition = new Vector2(0, 0);

        public void OnDrawGUI(DebugSystemDataScriptable systemData, DebugSystemGUIDataScriptable guiData)
        {
            DebugSystemDebug.DebugLog log;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            {
                for (int index = DebugSystemDebug.GetCount() - 1; index >= 0; index--)
                {
                    if (DebugSystemDebug.TryGetIndex(index, out log))
                    {
                        if (index < DebugSystemDebug.GetCount() - systemData.getLogMaximumLine)
                            break;

                        GUILayout.BeginHorizontal();
                        {
                            GUIStyle style = GUI.skin.GetStyle("LogTextStyle");
                            switch (log.GetLogType)
                            {
                                case LogType.Log:
                                    style.normal.textColor = Color.white;
                                    GUILayout.Box(guiData.imagesResources.logImage,
                                        GUILayout.Width(guiData.IconSize.x * systemData.scaleFactor),
                                        GUILayout.Height(guiData.IconSize.y * systemData.scaleFactor));
                                    break;

                                case LogType.Warning:
                                    style.normal.textColor = Color.yellow;
                                    GUILayout.Box(guiData.imagesResources.warningImage,
                                        GUILayout.Width(guiData.IconSize.x * systemData.scaleFactor),
                                        GUILayout.Height(guiData.IconSize.y * systemData.scaleFactor));
                                    break;

                                case LogType.Error:
                                    style.normal.textColor = Color.red;
                                    GUILayout.Box(guiData.imagesResources.errorImage,
                                        GUILayout.Width(guiData.IconSize.x * systemData.scaleFactor),
                                        GUILayout.Height(guiData.IconSize.y * systemData.scaleFactor));
                                    break;
                            }

                            GUILayout.Label(
                                "[" + log.GetLogTime + "]" + log.GetLogType + "\n" + log.GetMsg + "\n" + log.GetTrace,
                                style);
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        Debug.LogError("Failed : LogList Out Index Error");
                    }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.Space(10);
            if (GUILayout.Button("Clear Log", GUILayout.Height(Screen.height * 0.1f)))
                DebugSystemDebug.Clear();
        }
    }
}