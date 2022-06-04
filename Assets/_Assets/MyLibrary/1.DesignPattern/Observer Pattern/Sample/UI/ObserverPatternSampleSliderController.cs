using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary.DesignPattern.Sample
{
    public class ObserverPatternSampleSliderController : MonoBehaviour, IHpObserver, IMpObserver, IStaminaObserver
    {
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider mpSlider;
        [SerializeField] private Slider staminaSlider;

        private void Start()
        {
            ObserverControllerSample.Instance.GetObserverSubject<HpObserverSubject>().Subscribe(this);
            ObserverControllerSample.Instance.GetObserverSubject<MpObserverSubject>().Subscribe(this);
            ObserverControllerSample.Instance.GetObserverSubject<StaminaObserverSubject>().Subscribe(this);
        }

        public void Notify()
        {
            Debug.Log("Slider 스크립트에서 알림 호출");
        }

        public void HpChangeNotify(int hp)
        {
            float hpValue = Mathf.Clamp(hp, 0, 100f);
            hpSlider.value = hpValue * 0.01f;
        }
        
        public void MpChangeNotify(int mp)
        {
            float mpValue = Mathf.Clamp(mp, 0, 100f);
            mpSlider.value = mpValue * 0.01f;
        }
        
        public void StaminaChangeNotify(int stamina)
        {
            float staminaValue = Mathf.Clamp(stamina, 0, 100f);
            staminaSlider.value = staminaValue * 0.01f;
        }
    }
}