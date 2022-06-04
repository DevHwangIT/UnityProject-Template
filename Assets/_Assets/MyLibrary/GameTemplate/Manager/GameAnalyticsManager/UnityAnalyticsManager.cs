using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityAnalyticsManager : Singleton<UnityAnalyticsManager>
{
    private bool quitFlag = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        quitFlag = true;
        Analytics.CustomEvent("Application Quit", new Dictionary<string, object>
        {
            {"Is Application Quit?", quitFlag}
        });
    }
}
