using UnityEngine;

namespace MyLibrary.Utility.Sample
{
    public class ButtonEvents : MonoBehaviour
    {
        //Test Button Events

        public void HitByMonster()
        {
            DataCenter.Instance.hp.Value -= 10;
            Debug.Log("Hit By Monster");
        }

        public void ActiveFireBall()
        {
            DataCenter.Instance.mp.Value -= 10;
            Debug.Log("Active FireBall Skill");
        }

        public void RecoveryHp()
        {
            DataCenter.Instance.hp.Value += 10;
            Debug.Log("Recovery Hp");
        }

        public void RecoveryMp()
        {
            DataCenter.Instance.mp.Value += 10;
            Debug.Log("Recovery Mp");
        }
    }
}
