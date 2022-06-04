using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Tools
{
    [RequireComponent(typeof(DebugEventHandler), typeof(DebugSystemGUI))]
    public class DebugSystem : MonoBehaviour
    {
        private DebugEventHandler _debugEventHandler;

        public DebugEventHandler GetEventHandler
        {
            get
            {
                if (_debugEventHandler == null)
                    _debugEventHandler = this.GetComponent<DebugEventHandler>();
                return _debugEventHandler;
            }
        }
        private DebugSystemGUI _debugSystemGUI;
        public DebugSystemGUI GetDebugSystemGUI
        {
            get
            {
                if (_debugSystemGUI == null)
                    _debugSystemGUI = this.GetComponent<DebugSystemGUI>();
                return _debugSystemGUI;
            }
        }

        #region Singletone

        private static DebugSystem _instance;
        public static DebugSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (DebugSystem) FindObjectOfType(typeof(DebugSystem));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"Debug System (Singleton)");
                        _instance = singletonObject.AddComponent<DebugSystem>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        #endregion

        public void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            DebugSystemDebug.Run();
        }
    }
}