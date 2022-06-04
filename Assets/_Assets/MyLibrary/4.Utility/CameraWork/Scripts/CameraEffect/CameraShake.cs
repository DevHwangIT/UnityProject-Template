using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyLibrary.Utility
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MyLibrary/CameraWork/Camera Effects/Camera Shake")]
    [System.Serializable]
    public class CameraShake : CameraEffect
    {
        public bool isCompleteBackInitPos;
        private float _amount;
        
        private Vector3 InitPosition;

        #region Editor Variable
        private bool isInfinity = false;
        #endregion

        public CameraShake() : base()
        {
            _name = "Camera Shake";
            duration = 1f;
            _amount = 1f;
        }

#if UNITY_EDITOR
        public override void DrawInspectorGUI()
        {
            isCompleteBackInitPos = EditorGUILayout.Toggle("return after work is completed?", isCompleteBackInitPos);

            isInfinity = EditorGUILayout.Toggle("Duration is infinity?", isInfinity);
            if (isInfinity == false)
            {
                duration = EditorGUILayout.FloatField("Duration : ", duration);
            }
            else
            {
                duration = Mathf.Infinity;
            }

            _amount = EditorGUILayout.FloatField("Amount : ", _amount);
        }
#endif

        public override IEnumerator Action(Transform cam)
        {
            InitPosition = cam.transform.position;
            float timer=0;
            while (timer <= duration)
            {
                cam.localPosition = (Vector3) Random.insideUnitCircle * _amount + InitPosition;
                timer += Time.deltaTime;
                yield return null;
            }
            cam.transform.position = InitPosition;
            CamCoroutine = null;
        }

        public override void Stop(Transform cam)
        {
            if (isCompleteBackInitPos)
            {
                cam.transform.position = InitPosition;
            }
        }
    }
}