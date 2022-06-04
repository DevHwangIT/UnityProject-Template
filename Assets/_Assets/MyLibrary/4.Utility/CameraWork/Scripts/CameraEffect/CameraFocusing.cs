using System.Collections;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MyLibrary/CameraWork/Camera Effects/Camera Focusing")]
    [System.Serializable]
    public class CameraFocusing : CameraEffect
    {
        public bool isCompleteBackInitPos;
        public Transform TargetTransform;
        public float FollowSpeed;
        public bool isLookat;
        public Vector3 TargetToCamDistance;
        public float ZoomDistance;
        public float StartPointMoveDuration;
        public AnimationCurve _startCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve _returnCurve = AnimationCurve.Linear(0, 0, 1, 1);

        private Vector3 InitPosition;
        private float zoomAdditionDistance = 0f;

        #region Editor Variable

        private bool isInfinity = false;

        #endregion

        public CameraFocusing() : base()
        {
            _name = "Camera Focusing";
            isCompleteBackInitPos = false;
            duration = 1f;
            isLookat = false;
            TargetToCamDistance = new Vector3(0, 5, -5);
            FollowSpeed = 15f;
            ZoomDistance = -1.25f;
            StartPointMoveDuration = 0.15f;
            _startCurve = AnimationCurve.Linear(0, 0, 1, 1);
            _returnCurve = AnimationCurve.Linear(0, 0, 1, 1);
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

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Target Transform :");
            TargetTransform = (Transform) EditorGUILayout.ObjectField(TargetTransform, typeof(Transform), true);
            EditorGUILayout.EndHorizontal();

            isLookat = EditorGUILayout.Toggle("isLookAt? ", isLookat);
            TargetToCamDistance = EditorGUILayout.Vector3Field("Target To Camera Distance : ", TargetToCamDistance);
            FollowSpeed = EditorGUILayout.FloatField("Camera Follow Speed : ", FollowSpeed);
            ZoomDistance = EditorGUILayout.FloatField("Addition Zoom Distance : ", ZoomDistance);
            StartPointMoveDuration =
                EditorGUILayout.FloatField("Delay from start to move destnation : ", StartPointMoveDuration);
            _startCurve = EditorGUILayout.CurveField("Start Movement : ", _startCurve);
            _returnCurve = EditorGUILayout.CurveField("Return Movement : ", _returnCurve);
        }
#endif

        public IEnumerator ZoomCam(float zoomDelay)
        {
            zoomAdditionDistance = ZoomDistance;
            yield return new WaitForSeconds(zoomDelay);
            zoomAdditionDistance = 0;
        }

        public override IEnumerator Action(Transform cam)
        {
            if (TargetTransform != null)
            {
                InitPosition = cam.transform.position;
                float time = 0f;
                while (time < StartPointMoveDuration)
                {
                    Vector3 t_destPos = TargetTransform.position + TargetToCamDistance +
                                        (cam.forward * zoomAdditionDistance);
                    cam.position = Vector3.Lerp(cam.position, t_destPos,
                        _startCurve.Evaluate(time / StartPointMoveDuration));
                    time += Time.deltaTime;
                    yield return null;
                }

                time = 0;
                while (time < duration)
                {
                    if (isLookat)
                        cam.LookAt(TargetTransform);

                    Vector3 t_destPos = TargetTransform.position + TargetToCamDistance +
                                        (cam.forward * zoomAdditionDistance);
                    cam.position = Vector3.Lerp(cam.position, t_destPos, FollowSpeed * Time.deltaTime);
                    time += Time.deltaTime;
                    yield return null;
                }

                if (isCompleteBackInitPos)
                {
                    time = 0;
                    while (time < StartPointMoveDuration)
                    {
                        cam.position = Vector3.Lerp(cam.position, InitPosition,
                            _startCurve.Evaluate(time / StartPointMoveDuration));
                        time += Time.deltaTime;
                        yield return null;
                    }
                }
            }
            else
                Debug.LogError("Error - Target Transform Is Null in Member Field");
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
