using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace MyLibrary.Tools
{
    [InitializeOnLoad]
    public static class FristScenePlayer
    {
        static class ToolbarStyles
        {
            public static readonly GUIStyle commandButtonStyle;

            static ToolbarStyles()
            {
                commandButtonStyle = new GUIStyle("Command")
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleCenter,
                    imagePosition = ImagePosition.ImageAbove,
                    fontStyle = FontStyle.Bold
                };
            }
        }

        static FristScenePlayer()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();
            Texture icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                "Assets/MyLibrary/3.Tools/UnityToolbarExtender/FristScenePlayer/Icon/GameStart Icon.png");
            if (GUILayout.Button(new GUIContent(icon, "Start from system scene"), ToolbarStyles.commandButtonStyle))
            {
                PlayFromPrelaunchScene();
            }
        }

        public static void PlayFromPrelaunchScene()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            if (EditorSceneManager.sceneCountInBuildSettings != 0)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
                EditorApplication.isPlaying = true;
            }
        }
    }
}