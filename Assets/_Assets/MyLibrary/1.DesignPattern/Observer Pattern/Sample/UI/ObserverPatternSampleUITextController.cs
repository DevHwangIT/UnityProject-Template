using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary.DesignPattern.Sample
{
    public class ObserverPatternSampleUITextController : MonoBehaviour, IHpObserver, IMpObserver, IStaminaObserver
    {
        [SerializeField] private Text hpText;
        [SerializeField] private Text mpText;
        [SerializeField] private Text staminaText;

        private void Start()
        {
            ObserverControllerSample.Instance.GetObserverSubject<HpObserverSubject>().Subscribe(this);
            ObserverControllerSample.Instance.GetObserverSubject<MpObserverSubject>().Subscribe(this);
            ObserverControllerSample.Instance.GetObserverSubject<StaminaObserverSubject>().Subscribe(this);
        }

        public void Notify()
        {
            Debug.Log("Text 스크립트에서 알림 호출");
        }

        public void HpChangeNotify(int hp)
        {
            hpText.text = hp + " / 100";
        }
        
        public void MpChangeNotify(int mp)
        {
            mpText.text = mp + " / 100";
        }
        
        public void StaminaChangeNotify(int stamina)
        {
            staminaText.text = stamina + " / 100";
        }
    }
}