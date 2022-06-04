using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEngine;
using UnityEngine.Events;

public enum keyEventType
{
    GetKeyDown,
    GetKey,
    GetKeyUp
}

[DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(PlayerInput))]
public class InputEventManager : Singleton<InputEventManager>
{
    private int[] keyCodeArray;
    private static Dictionary<KeyCode, UnityEvent> keyDownEvent = new Dictionary<KeyCode, UnityEvent>();
    private static Dictionary<KeyCode, UnityEvent> keyEvent = new Dictionary<KeyCode, UnityEvent>();
    private static Dictionary<KeyCode, UnityEvent> keyUpEvent = new Dictionary<KeyCode, UnityEvent>();

    private static Dictionary<KeyCode, UnityEvent> SelectEventTypeDictionary(keyEventType eventType)
    {
        switch (eventType)
        {
            case keyEventType.GetKeyDown: return keyDownEvent;
            case keyEventType.GetKeyUp: return keyEvent;
            case keyEventType.GetKey: return keyUpEvent;
            default: return keyEvent;
        }
    }
    
    public static void Add(KeyCode key, UnityEvent method, keyEventType eventType = keyEventType.GetKey)
    {
        Dictionary<KeyCode, UnityEvent> eventDictionary = SelectEventTypeDictionary(eventType);
        if (eventDictionary.ContainsKey(key))
            eventDictionary[key].AddListener(() => { method.Invoke(); });
        else
            eventDictionary.Add(key, method);
    }

    public static void Remove(KeyCode key, UnityEvent method, keyEventType eventType = keyEventType.GetKey)
    {
        Dictionary<KeyCode, UnityEvent> eventDictionary = SelectEventTypeDictionary(eventType);
        if (eventDictionary.ContainsKey(key))
            eventDictionary[key].RemoveListener(() => { method.Invoke(); });
    }

    public static void ChangeKey(KeyCode preKey, KeyCode key, UnityEvent method, keyEventType eventType = keyEventType.GetKey)
    {
        Remove(preKey, method, eventType);
        Add(key, method, eventType);
    }

    public static void Clear(KeyCode key, keyEventType eventType = keyEventType.GetKey)
    {
        Dictionary<KeyCode, UnityEvent> eventDictionary = SelectEventTypeDictionary(eventType);
        if (eventDictionary.ContainsKey(key))
            eventDictionary[key] = null;
    }

    void Awake() 
    {
        keyCodeArray = (int[])System.Enum.GetValues(typeof(KeyCode));
    }
 
    void Update() 
    {
        if (Input.anyKey)
        {
            for (int i = 0; i < keyCodeArray.Length; i++)
            {
                KeyCode eKeyCode = (KeyCode) keyCodeArray[i];
                if (Input.GetKeyDown(eKeyCode) && keyDownEvent.ContainsKey(eKeyCode))
                    keyDownEvent[eKeyCode]?.Invoke();
                if (Input.GetKey(eKeyCode) && keyEvent.ContainsKey(eKeyCode))
                    keyEvent[eKeyCode]?.Invoke();
                if (Input.GetKeyUp(eKeyCode) && keyUpEvent.ContainsKey(eKeyCode))
                    keyUpEvent[eKeyCode]?.Invoke();
            }
        }
    }
}
