using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    public class ObserverPatternSampleUIButtons : MonoBehaviour
    {
        [SerializeField] private ObserverPatternCharacterStatSample characterStat;
        
        public void HpIncrementBtn()
        {
            int hp = characterStat.Hp + 10;
            characterStat.Hp = Mathf.Clamp(hp, 0, 100);
        }
        public void HpDecrementBtn()
        {
            int hp = characterStat.Hp - 10;
            characterStat.Hp = Mathf.Clamp(hp, 0, 100);
        }
        
        public void MpIncrementBtn()
        {
            int mp = characterStat.Mp + 10;
            characterStat.Mp = Mathf.Clamp(mp, 0, 100);
        }
        public void MpDecrementBtn()
        {
            int mp = characterStat.Mp - 10;
            characterStat.Mp = Mathf.Clamp(mp, 0, 100);
        }
        
        public void StaminaIncrementBtn()
        {
            int stamina = characterStat.Stamina + 10;
            characterStat.Stamina = Mathf.Clamp(stamina, 0, 100);
        }
        public void StaminaDecrementBtn()
        {
            int stamina = characterStat.Stamina - 10;
            characterStat.Stamina = Mathf.Clamp(stamina, 0, 100);
        }
    }
}
