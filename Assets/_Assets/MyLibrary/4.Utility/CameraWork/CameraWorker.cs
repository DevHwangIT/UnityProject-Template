using Cinemachine;
using UnityEngine;

namespace MyLibrary.Utility
{
    [System.Serializable]
    public enum CameraWorkType
    {
        Unity_Camera,
        Cinemachine_Camera
    }

    [ExecuteInEditMode]
    public class CameraWorker : MonoBehaviour
    {
        public CameraWorkType cameraType;
        public CameraEffectsData EffectsData;
        public Transform GetCamera
        {
            get
            {
                switch (cameraType)
                {
                    case CameraWorkType.Cinemachine_Camera:
                        CinemachineBrain camBrain = FindObjectOfType<CinemachineBrain>();
                        if (camBrain.ActiveVirtualCamera as CinemachineVirtualCamera)
                            return (camBrain.ActiveVirtualCamera as CinemachineVirtualCamera).transform;
                        else
                            break;

                    case CameraWorkType.Unity_Camera:
                        if (Camera.main != null)
                            return Camera.main.transform;
                        else
                            break;
                }
#if UNITY_EDITOR
                Debug.LogWarning("Please Check the camera type or camera object");
#endif
                return null;
            }
        }

        private void Awake()
        {
            if (Camera.main == null || this.gameObject != Camera.main.gameObject)
                Destroy(this);
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            Camera mainCam = Camera.main;
            if (this.gameObject != mainCam.gameObject)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (this != null)
                    {
                        Debug.Log("Only MainCamera can have only one of these scripts.");
                        if (mainCam.transform.GetComponent<CameraWorker>() == null)
                            mainCam.gameObject.AddComponent<CameraWorker>();

                        if (GetComponent<CameraWorker>() && mainCam.transform != this.transform)
                            DestroyImmediate(this);
                    }
                };
            }
            else
            {
                Debug.LogWarning("Warring - Main Camera Tag or Main Camera Object is Null");
            }
#endif
        }
        
        public void Action<T>() where T : CameraEffect
        {
            CameraEffect effect = EffectsData.GetCameraEffect<T>();
            if (effect != null)
            {
                if (effect.isPlaying)
                    Stop<T>();
                effect.CamCoroutine = StartCoroutine(effect.Action(GetCamera));
            }
        }

        public void Action(CameraEffect type)
        {
            CameraEffect effect = EffectsData.GetCameraEffect(type);
            if (effect != null)
            {
                if (effect.isPlaying)
                    Stop(type);
                effect.CamCoroutine = StartCoroutine(effect.Action(GetCamera));
            }
        }

        public void Stop<T>() where T : CameraEffect
        {
            CameraEffect effect = EffectsData.GetCameraEffect<T>();
            if (effect != null)
            {
                if (effect.isPlaying)
                {
                    StopCoroutine(effect.CamCoroutine);
                    effect.CamCoroutine = null;
                    effect.Stop(GetCamera);
                }
            }
        }

        public void Stop(CameraEffect type)
        {
            CameraEffect effect = EffectsData.GetCameraEffect(type);
            if (effect != null)
            {
                if (effect.isPlaying)
                {
                    StopCoroutine(effect.CamCoroutine);
                    effect.CamCoroutine = null;
                    effect.Stop(GetCamera);
                }
            }
        }
    }
}
