using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DebugSystemData", menuName = "ScriptableObjects/MyLibrary/Tools/Debug/DebugSystemData", order = 1)]
public class DebugSystemDataScriptable : ScriptableObject
{
    [Header("Value")]
    [Range(0, 2)]
    public float scaleFactor = 1;
    
    [Header("System")]
    [SerializeField] private bool isDebugActive = false;
    [SerializeField] private string passWord;
    
    [Header("Log")]
    [SerializeField] private int LogMaximumLine = 50;

    public int getLogMaximumLine => LogMaximumLine;
    public bool getisDebugActive => isDebugActive;
    
    public bool ComparePwKey(string inputPw)
    {
        return passWord.Equals(inputPw);
    }
}
