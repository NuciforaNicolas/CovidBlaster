using UnityEngine;

namespace Controllers
{
    public class OrganismRandomMovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed, movementDelay;
        [SerializeField] private Vector2 movementDistanceRange;

        private Vector2 _movementTarget;
        private Rigidbody2D _rigidbody;
        private float _timeSinceStopped;
        private bool _hasStopped;

        private void Awake()
        {
            _movementTarget = transform.position;
            _rigidbody = GetComponent<Rigidbody2D>();
            _timeSinceStopped = Time.time;
        }

        private void FixedUpdate()
        {
            if ((_rigidbody.position - _movementTarget).magnitude > 0.0001f)
            {
                Vector2 playerPosition = _rigidbody.position;
                Vector2 targetOffset = _movementTarget - playerPosition;
                _rigidbody.MovePosition(playerPosition +
                                        Vector2.ClampMagnitude(targetOffset, movementSpeed * Time.fixedDeltaTime));
            }
            else if (!_hasStopped)
            {
                _hasStopped = true;
                _timeSinceStopped = Time.time;
            }
            else if (Time.time > _timeSinceStopped + movementDelay)
            {
                GetNewMovementTarget();
                _hasStopped = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            GetNewMovementTarget();
        }

        public void TeleportOrganism(Vector3 targetPosition)
        {
            _hasStopped = true;
            _timeSinceStopped = Time.time;
            transform.position = targetPosition;
            _movementTarget = _rigidbody.position;
        }

        private void GetNewMovementTarget()
        {
            Vector2 movementOffset = Random.insideUnitCircle.normalized *
                                     Random.Range(movementDistanceRange.x, movementDistanceRange.y);
            _movementTarget = (Vector2) transform.position + movementOffset;
        }
    }
}