using UnityEngine;

namespace Controllers
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float damage, speed, explosionRadius, explosionDamage;

        private bool _setToDestroy;

        private void Start()
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        }

        private void Update()
        {
            if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100)
            {
                _setToDestroy = true;
            }
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
            if (!other.collider.tag.Equals("Player"))
            {
                HealthController healthController = other.gameObject.GetComponent<HealthController>();

                if (healthController != null)
                {
                    healthController.DealDamage(damage);
                }

                _setToDestroy = true;
            }
        }
    }
}