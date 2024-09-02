using System.Collections;
using UnityEngine;

namespace CasualLevelsDemo
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Transform shootingPosition;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float betweenShootDelay = 1.0f;
        [SerializeField] private float projectileDelay = 0.2f;
        [SerializeField] private float projectileSpeed = 3.0f;
        [SerializeField] private float projectileMass = 10000.0f;
        [SerializeField] private float projectileScale = 3.0f;

        private void OnEnable()
        {
            StartCoroutine(ShootCycle());
        }

        private IEnumerator ShootCycle()
        {
            var animator = GetComponent<Animator>();
            yield return new WaitForSeconds(Random.value);
            var betweenShootDelay = new WaitForSeconds(this.betweenShootDelay);
            var projectileDelay = new WaitForSeconds(this.projectileDelay);
            var gunColliders = GetComponentsInChildren<Collider>();
            while (true)
            {
                animator.Play("Shoot");

                yield return projectileDelay;

                var instance = Instantiate(projectilePrefab, shootingPosition.position, shootingPosition.rotation);
                GameObject.Destroy(instance, 10);
                instance.transform.localScale = Vector3.one * projectileScale;
                instance.AddComponent<Stunner>();
                var collier = instance.GetComponent<Collider>();
                foreach (var gunCollider in gunColliders)
                    Physics.IgnoreCollision(collier, gunCollider);

                var rigidBody = instance.GetComponent<Rigidbody>();
                rigidBody.mass = projectileMass;
                rigidBody.linearVelocity = shootingPosition.forward * projectileSpeed;

                yield return betweenShootDelay;
            }
        }
    }
}
