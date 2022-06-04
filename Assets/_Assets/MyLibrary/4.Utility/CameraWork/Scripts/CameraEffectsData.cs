using System.Collections.Generic;
using UnityEngine;


namespace MyLibrary.Utility
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MyLibrary/CameraWork/Camera EffectData")]
    public class CameraEffectsData : ScriptableObject
    {
        [SerializeField] public CameraEffect[] Effects;
        public CameraEffect[] GetArrayEffect => Effects;
        public CameraEffect GetCameraEffect<T>() where T : CameraEffect
        {
            foreach (var effect in GetArrayEffect)
            {
                if (effect.GetType() == typeof(T))
                    return effect;
            }
            return null;
        }

        public CameraEffect GetCameraEffect(CameraEffect type)
        {
            foreach (var effect in GetArrayEffect)
            {
                if (effect.GetType() == type.GetType())
                    return effect;
            }
            return null;
        }
    }
}