using UnityEngine;

namespace CasualLevelsDemo
{
    [RequireComponent(typeof(Animator))]
    public class AnimationRandomizer : MonoBehaviour
    {
        private void Start()
        {
            var animator = GetComponent<Animator>();

            var currentState = animator.GetCurrentAnimatorStateInfo(0);
            
            animator.Play(currentState.shortNameHash, 0, Random.value); 
        }
    }
}