using System;
using Enums.Sound;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Controllers
{
    public class OrganismRandomMovementController : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed, movementDelay;

        [SerializeField]
        private Vector2 movementDistanceRange;

        [SerializeField]
        private UnitName unitName;

        private Vector2 _movementTarget;
        private Rigidbody2D _rigidbody;
        private float _timeSinceStopped;
        private bool _hasStopped;
        private EventInstance _movingSound;

        private void Awake()
        {
            _movementTarget = transform.position;
            _rigidbody = GetComponent<Rigidbody2D>();
            _timeSinceStopped = Time.time;
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude > 0.001f)
            {
                if (!_movingSound.isValid())
                {
                    _movingSound = RuntimeManager.CreateInstance("event:/Sounds/Movement");
                }
            }
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
            PlayerMovementController playerMovementController =
                other.collider.gameObject.GetComponent<PlayerMovementController>();
            if (playerMovementController != null)
            {
                if (UnitName.Virus.Equals(unitName))
                {
                    RuntimeManager.PlayOneShot("event:/Sounds/BumpIntoGreen", transform.position);
                }
                else if (UnitName.RedCell.Equals(unitName))
                {
                    RuntimeManager.PlayOneShot("event:/Sounds/BumpIntoRed", transform.position);
                }
            }
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