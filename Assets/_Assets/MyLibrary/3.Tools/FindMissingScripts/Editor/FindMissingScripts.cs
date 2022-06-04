using UnityEngine;
using UnityEditor;
public class FindMissingScripts : EditorWindow
{
    [MenuItem("MyLibrary/Tools/Create FindMissingScript Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(FindMissingScripts));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in selected prefabs"))
        {
            FindInSelected();
        }

        GameObject[] go = Selection.gameObjects;
        foreach (var g in go)
        {
            GUILayout.Label(g.gameObject.name);
        }
    }

    private static void FindInSelected()
    {
        GameObject[] go = Selection.gameObjects;
        foreach (var selectedObj in go)
        {
            foreach (var gObj in selectedObj.GetComponentsInChildren<Transform>(true))
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gObj.gameObject);
            }
        }
    }
}