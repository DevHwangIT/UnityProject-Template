using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    public class HpObserverSubject : ObserverSubject
    {
        public void HpNotify(int hp)
        {
            Debug.Log("Hp Change Notice!");
            foreach (var observer in Observers)
            {
                ((IHpObserver) observer).HpChangeNotify(hp);
            }
        }
    }

    public class MpObserverSubject : ObserverSubject
    {
        public void MpNotify(int mp)
        {
            Debug.Log("Mp Change Notice!");
            foreach (var observer in Observers)
            {
                ((IMpObserver) observer).MpChangeNotify(mp);
            }
        }
    }

    public class StaminaObserverSubject : ObserverSubject
    {
        public void StaminaNotify(int stamina)
        {
            Debug.Log("Stamina Change Notice!");
            foreach (var observer in Observers)
            {
                ((IStaminaObserver) observer).StaminaChangeNotify(stamina);
            }
        }
    }
}