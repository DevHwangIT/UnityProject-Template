using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
//상태 정의
    public partial class FSMExampleCharacter : MonoBehaviour
    {
        public class IdleState : FSMType<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                
            }

            public override void Update(FSMExampleCharacter body)
            {
                
            }

            public override void Exit(FSMExampleCharacter body)
            {
                
            }
        }

        public class RunState : FSMType<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsRun", true);
            }

            public override void Update(FSMExampleCharacter body)
            {
                
            }

            public override void Exit(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsRun", false);
            }
        }

        public class WalkState : FSMType<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsWalk", true);
            }

            public override void Update(FSMExampleCharacter body)
            {
                
            }

            public override void Exit(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsWalk", false);
            }
        }
        
        public class JumpState : FSMType<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                
            }

            public override void Update(FSMExampleCharacter body)
            {
                body._animator.SetTrigger("IsJump");
            }

            public override void Exit(FSMExampleCharacter body)
            {
                
            }
        }
    }
}
