using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace CasualLevelsDemo
{
    public interface IVelocityProvider
    {
        Vector3 GetVelocity(Vector3 worldPosition);
    }
    
    public class MovablePlatform : MonoBehaviour, IVelocityProvider
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float pauseDuration = 0.5f;
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;

        private Vector3 velocity = Vector3.zero;

        public Vector3 GetVelocity(Vector3 worldPosition)
        {
            return velocity;
        }

        private IEnumerator Start()
        {
            var target = pointA;
            var rb = GetComponent<Rigidbody>();
            var pauseDelay = new WaitForSeconds(pauseDuration);
            var fixedUpdateDelay = new WaitForFixedUpdate();
            yield return fixedUpdateDelay;
            while (true)
            {
                while (Vector3.Distance(transform.position, target.position) > 0.05f)
                {
                    var newPosition = Vector3.MoveTowards(transform.position, target.position, Time.fixedDeltaTime * speed);
                    velocity = (newPosition - transform.position).normalized * speed;
                    rb.MovePosition(newPosition);

                    yield return fixedUpdateDelay;
                }

                target = target == pointA ? pointB : pointA;
                velocity = Vector3.zero;
                yield return pauseDelay;
            }
        }
    }
}