using System;
using UnityEngine;

namespace Controllers
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float damage, speed;

        private bool _setToDestroy;

        private void Start()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }

        private void LateUpdate()
        {
            if (_setToDestroy)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HealthController healthController = other.gameObject.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.DealDamage(damage);
                _setToDestroy = true;
            }
        }
    }
}