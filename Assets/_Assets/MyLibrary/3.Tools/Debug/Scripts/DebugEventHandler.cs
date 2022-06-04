using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MyLibrary.Tools
{
    [System.Serializable]
    public struct CheatEvent
    {
        [Tooltip("이벤트 이름")]
        [SerializeField] private string _name;
        [Tooltip("이벤트 설명")]
        [SerializeField] private string _info;
        [Tooltip("이벤트 호출 콜백")]
        [SerializeField] private UnityEvent _debugEventCallback;

        public string GetName => _name;
        public string GetInfo => _info;

        public CheatEvent(string name, UnityEvent callback)
        {
            _name = name;
            _info = "";
            _debugEventCallback = callback;
        }

        public CheatEvent(string name, string info, UnityEvent callback)
        {
            _name = name;
            _info = info;
            _debugEventCallback = callback;
        }

        public void CallCheatEvent()
        {
            _debugEventCallback?.Invoke();
        }
    }

    public class DebugEventHandler : MonoBehaviour
    {
        [SerializeField] private List<CheatEvent> debugEventList = new List<CheatEvent>();
        public List<CheatEvent> GetCheatList => debugEventList;

        public int Count { get { return debugEventList.Count; } }
        public void Add(CheatEvent _cheat) { debugEventList.Add(_cheat); }
        
        public bool TryGetIndex(int index, out CheatEvent _cheat)
        {
            _cheat = new CheatEvent();
            if (debugEventList.Count <= index)
            {
                return false;
            }
            else
            {
                _cheat = debugEventList[index];
                return true;
            }
        }
    }
}
