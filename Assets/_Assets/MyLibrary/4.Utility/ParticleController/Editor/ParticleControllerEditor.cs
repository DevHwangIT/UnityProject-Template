using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    [CustomEditor(typeof(ParticleController))]
    public class ParticleControllerEditor : Editor
    {
        private ParticleController _controller;

        private void OnEnable()
        {
            _controller = (ParticleController) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(5);
            _controller.playConditionType = (PlayCondition) EditorGUILayout.EnumPopup("Particle Play Condition : ", _controller.playConditionType);
            
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Particle Property");
            
            _controller.hideType = (HideType) EditorGUILayout.EnumPopup("Hide Type : ", _controller.hideType);
            if (_controller.hideType == HideType.ObjectPooling)
            {
                EditorGUILayout.Space(5);
                SerializedProperty getToPoolEvent = serializedObject.FindProperty("onGetToPool");
                SerializedProperty returnToPoolEvent = serializedObject.FindProperty("onReturnToPool");
                EditorGUIUtility.LookLikeControls();
                EditorGUILayout.PropertyField(getToPoolEvent);
                EditorGUILayout.PropertyField(returnToPoolEvent);
            }
            
            _controller.destroyType = (DestroyCondition) EditorGUILayout.EnumPopup("Destory Type : ", _controller.destroyType);
            EditorGUILayout.Space(5);
            switch (_controller.destroyType)
            {
                case DestroyCondition.Distance:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Distance Compare Target : ");
                    _controller.distanceCompareTarget = (Transform) EditorGUILayout.ObjectField(_controller.distanceCompareTarget, typeof(Transform));
                    EditorGUILayout.EndHorizontal();
                    _controller.distanceValue = EditorGUILayout.FloatField("Destory Condition Distance : ", _controller.distanceValue);
                    break;

                case DestroyCondition.Time:
                    _controller.durationTime = EditorGUILayout.FloatField("Destory Condition Destruction Time : ", _controller.durationTime);
                    break;
            }
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
