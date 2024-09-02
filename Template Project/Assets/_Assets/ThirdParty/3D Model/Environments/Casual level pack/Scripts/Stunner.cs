using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasualLevelsDemo
{
    public class Stunner : MonoBehaviour
    {
        [SerializeField] private float stunDuration = 0.5f;
        [SerializeField] private float minStunImpulse = 2000.0f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.impulse.magnitude < minStunImpulse)
                return;

            var character = collision.collider.GetComponent<Character>();
            if (character == null)
                return;

            character.Stun(stunDuration);
        }
    }
}