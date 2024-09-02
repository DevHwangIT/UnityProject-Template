using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationStarter : MonoBehaviour
{
    public Action OnBeforeStartCallback;
    
    private void Start()
    {
        StartCoroutine(LoadingRoutine());
    }

    IEnumerator LoadingRoutine()
    {
        OnBeforeStartCallback?.Invoke();
        yield return null;
        GameSceneController.CurrentScene = GameScene.Main;
    }
}
