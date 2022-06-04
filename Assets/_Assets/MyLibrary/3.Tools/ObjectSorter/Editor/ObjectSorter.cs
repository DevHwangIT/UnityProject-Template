using System;
using UnityEngine;
using UnityEditor;

public class ObjectSorter : EditorWindow
{
    private static Vector3 objStartingPosition;
    private static Vector3 objSortInterval;

    [MenuItem("MyLibrary/Tools/Create ObjectSort Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ObjectSorter));
    }

    public void OnGUI()
    {
        objStartingPosition = EditorGUILayout.Vector3Field("Object Start Position : ", objStartingPosition);
        objSortInterval = EditorGUILayout.Vector3Field("Object Sort Interval : ", objSortInterval);

        if (GUILayout.Button("Sort Work"))
        {
            SortInSelected();
        }

        EditorGUILayout.LabelField("- Slected Object List");
        GameObject[] selectedObjects = Selection.gameObjects;

        Array.Sort(selectedObjects, (a, b) => (a.transform.GetSiblingIndex() < b.transform.GetSiblingIndex()) ? -1 : 1);
        
        foreach (var selectedObject in selectedObjects)
        {
            GUILayout.Label(selectedObject.gameObject.name);
        }
    }

    private static void SortInSelected()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        Array.Sort(selectedObjects, (a, b) => (a.transform.GetSiblingIndex() < b.transform.GetSiblingIndex()) ? -1 : 1);
        for (int index = 0; index < selectedObjects.Length; index++)
        {
            selectedObjects[index].transform.localPosition = objStartingPosition + objSortInterval * index;
        }
    }
}