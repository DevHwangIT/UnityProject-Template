using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Networking;

enum NetworkState
{
    Wifi,
    DataNetwork,
    OtherNetworks,
    DisConnected
}
public class NetworkStateChecker : Singleton<NetworkStateChecker>
{
    [SerializeField] private string checkTargetUrl = "https://www.google.com/";
    private NetworkState m_networkState = NetworkState.DisConnected;
    private Coroutine NetworkCheckCoroutine = null;

    public Action OnNetworkDisconnectedCallback;

    public bool IsConnected
    {
        get
        {
            if (m_networkState == NetworkState.DisConnected)
                return false;
            return true;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            DontDestroyOnLoad(Instance.gameObject);
    }

    private void Update()
    { 
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                m_networkState = NetworkState.DataNetwork;
            else
                m_networkState = NetworkState.Wifi;
        }
        else
        {
            // Hotspot 등 다른 네트워크 형식 연결 일 경우에도 NotReachable로 처리됨.
            if (NetworkCheckCoroutine == null)
                NetworkCheckCoroutine = StartCoroutine(NetworkCheck());
        }
    }
    
    IEnumerator NetworkCheck()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(checkTargetUrl);
        
        yield return webRequest.SendWebRequest();
        
        if (webRequest.error == null) 
        {
            m_networkState = NetworkState.OtherNetworks;
        }
        else
        {
            m_networkState = NetworkState.DisConnected;
            OnNetworkDisconnectedCallback?.Invoke();
        }
        NetworkCheckCoroutine = null;
    }
}
