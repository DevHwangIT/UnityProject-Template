using System;
using MyLibrary.Attribute;
using UnityEngine;

namespace MyLibrary.DesignPattern
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T) FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(T)} (Singleton)");
                        _instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (Instance != this)
                Destroy(this.gameObject);
        }
    }
}