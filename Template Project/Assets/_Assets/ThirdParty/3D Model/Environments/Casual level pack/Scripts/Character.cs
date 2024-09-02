using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasualLevelsDemo
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CapsuleCollider capsuleCollider;
        [SerializeField] private float movementSpeed = 5.0f;
        [SerializeField] private Rigidbody rigidbody;
        private float stunEndTime = 0.0f;
        private Vector3 startPosition;
        private Quaternion startRotation;
        private Vector3? targetVelocity;
        
        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        public void Stun(float duration)
        {
            if (duration < 0)
                throw new ArgumentException("Can't apply negative stun duration");

            stunEndTime = Mathf.Max(stunEndTime, Time.time + duration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<RespawnZone>())
                return;

            transform.position = startPosition;
            transform.rotation = startRotation;
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            RaycastHit hit;
            var isGrounded = Physics.Raycast(new Ray(transform.position + capsuleCollider.center, Vector3.down),
                                             out hit, 
                                             capsuleCollider.height * 0.5f + 0.25f);

            if (Time.time < stunEndTime || !isGrounded)
            {
                this.targetVelocity = null;
                animator.SetFloat("Speed", 0.0f, 0.15f, Time.deltaTime);
                return;
            }

            var velocityProvider = hit.collider.GetComponentInParent<IVelocityProvider>();
            var additionalVelocity = Vector3.zero;
            if (velocityProvider != null)
                additionalVelocity += velocityProvider.GetVelocity(transform.position);

            var characterTrigger = hit.collider.GetComponentInParent<ICharacterFloorTrigger>();
            if (characterTrigger != null)
                characterTrigger.OnCharacterTrigger(this);

            var camera = Camera.main;
            var right = camera.transform.right;
            var forward = camera.transform.forward;
            right.y = 0;
            forward.y = 0;
            
            var transformedInput = Input.GetAxisRaw("Horizontal") * right + Input.GetAxisRaw("Vertical") * forward;

            var velocity = rigidbody.linearVelocity;

            var cachedYVelocity = velocity.y;

            var targetVelocity = new Vector3(transformedInput.x * movementSpeed,
                                             cachedYVelocity,
                                             transformedInput.z * movementSpeed);

            var planarVelocity = targetVelocity;
            planarVelocity.y = 0;
            if (planarVelocity.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(planarVelocity.normalized, Vector3.up), Time.deltaTime * 10.0f);

            this.targetVelocity = planarVelocity + additionalVelocity + Vector3.up * Mathf.Min(0.0f, cachedYVelocity);
            animator.SetFloat("Speed", planarVelocity.magnitude / movementSpeed, 0.15f, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!targetVelocity.HasValue)
                return;

            rigidbody.linearVelocity = targetVelocity.Value;
        }
    }
}
