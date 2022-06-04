using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    [CustomEditor(typeof(CameraWorker))]
    public class CameraWorkerEditor : Editor
    {
        private CameraWorker camWorker;

        void OnEnable()
        {
            camWorker = (CameraWorker) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(1f);
            EditorGUILayout.LabelField("Camera Type : ");
            camWorker.cameraType = (CameraWorkType) EditorGUILayout.EnumPopup(camWorker.cameraType);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5f);

            CameraEffect[] effects = camWorker.EffectsData.GetArrayEffect;
            
            for (int index = 0; index < effects.Length; index++)
            {
                EditorGUILayout.Space(2.5f);

                EditorGUILayout.BeginHorizontal();
                effects[index].isInspectorShown = EditorGUILayout.ToggleLeft(effects[index].ClassName, effects[index].isInspectorShown);
                EditorGUILayout.EndHorizontal();

                if (!effects[index].isInspectorShown)
                    continue;
                
                EditorGUILayout.BeginVertical("Box");
                effects[index].DrawInspectorGUI();
                
                if (EditorApplication.isPlaying)
                {
                    if (effects[index].isPlaying == false)
                    {
                        if (GUILayout.Button("Play"))
                            camWorker.Action(effects[index]);
                    }
                    else
                    {
                        if (GUILayout.Button("Stop"))
                            camWorker.Stop(effects[index]);
                    }
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(2.5f);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
