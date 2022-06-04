using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    public class ObserverPatternCharacterStatSample : MonoBehaviour
    {
        private int _hp = 100;
        public int Hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                ObserverControllerSample.Instance.GetObserverSubject<HpObserverSubject>().HpNotify(_hp);
            }
        }
        
        private int _mp = 100;
        public int Mp
        {
            get
            {
                return _mp;
            }
            set
            {
                _mp = value;
                ObserverControllerSample.Instance.GetObserverSubject<MpObserverSubject>().MpNotify(_mp);
            }
        }
        
        private int _stamina = 100;    
        public int Stamina
        {
            get
            {
                return _stamina;
            }
            set
            {
                _stamina = value;
                ObserverControllerSample.Instance.GetObserverSubject<StaminaObserverSubject>().StaminaNotify(_stamina);
            }
        }
    }
}