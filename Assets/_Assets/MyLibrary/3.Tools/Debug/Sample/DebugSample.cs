using UnityEngine;
using UnityEngine.Events;

namespace MyLibrary.Tools.Sample
{
    public class DebugSample : MonoBehaviour
    {
        private void Start()
        {
            TEST();
        }

        private void TEST()
        {
            DebugEventHandler eventHandler = DebugSystem.Instance.GetEventHandler;
            
            UnityEvent testevent1 = new UnityEvent();
            testevent1.AddListener(() => { Debug.Log("돈 증가 치트 입니다."); });
            eventHandler.Add(new CheatEvent("돈 증가 치트", "돈을 일정만큼 증가시킵니다.", testevent1));
            
            UnityEvent testevent2 = new UnityEvent();
            testevent2.AddListener(() => { Debug.LogWarning("테스트 경고 로그"); });
            eventHandler.Add(new CheatEvent("경고 로그 발생", "테스트 경고 로그.", testevent2));
            
            UnityEvent testevent3 = new UnityEvent();
            testevent3.AddListener(() => { Debug.LogError("테스트 에러 로그"); });
            eventHandler.Add(new CheatEvent("경고 에러 발생", "테스트 에러 로그.", testevent3));
        }
    }
}
