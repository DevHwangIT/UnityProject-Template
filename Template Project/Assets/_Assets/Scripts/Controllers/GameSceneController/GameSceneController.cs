using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene : uint
{
    Init = 0,
    Main = 1
}

public class GameSceneController : MonoBehaviour
{
    private static GameScene _currentGameScene = GameScene.Init;
    
    //변경 전 장면, 변경 후 장면
    public static Action<GameScene, GameScene> OnBeforeChangedSceneCallback;
    
    public static GameScene CurrentScene
    {
        get
        {
            return _currentGameScene;
        }
        set
        {
            OnBeforeChangedSceneCallback?.Invoke(_currentGameScene, value);
            _currentGameScene = value;
            OnBeforeChangedSceneCallback = null;
            SceneManager.LoadScene((int)_currentGameScene);
        }
    }
}
