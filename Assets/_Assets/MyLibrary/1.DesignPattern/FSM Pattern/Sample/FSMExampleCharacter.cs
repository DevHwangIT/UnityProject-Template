using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    public partial class FSMExampleCharacter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public FSMType<FSMExampleCharacter> ChangeFSM
        {
            set { nextFSMState = value; }
        }

        private FSMType<FSMExampleCharacter> currentFSMState;
        private FSMType<FSMExampleCharacter> nextFSMState;

        protected void Awake()
        {
            currentFSMState = new IdleState();
            nextFSMState = currentFSMState;
            currentFSMState.Enter(this);
        }

        protected void Update()
        {
            currentFSMState.Update(this);

            if ((currentFSMState.GetType() == nextFSMState.GetType()) == false) 
            {
                currentFSMState.Exit(this);
                currentFSMState = nextFSMState;
                currentFSMState.Enter(this);
            }
        }
    }
}