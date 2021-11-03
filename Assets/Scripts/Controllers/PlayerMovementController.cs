using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float playerVelocity, playerRotationSpeed;
        [SerializeField] private bool shouldUseRotationSpeed;

        private float _horizontalInput, _verticalInput;
        private Vector2 _lookAtTarget;

        public Vector2 LookAtTarget => _lookAtTarget;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _horizontalInput = 0;
            _verticalInput = 0;
        }

        private void Start()
        {
            _lookAtTarget = GetLookAtPosition();
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            
            _lookAtTarget = GetLookAtPosition();
        }

        private void FixedUpdate()
        {
            // Move horizontally and vertically
            _rigidbody.velocity = new Vector2(_horizontalInput, _verticalInput) * playerVelocity;

            // Turn
            float targetAngle = CalculateViewAtAngle();
            // UnityEngine.Debug.Log(targetAngle);
            if (shouldUseRotationSpeed)
            {
                if (targetAngle >= 0)
                {
                    targetAngle = Mathf.Min(targetAngle, playerRotationSpeed * Time.fixedDeltaTime);
                } else if (targetAngle < 0)
                {
                    targetAngle = Mathf.Max(targetAngle, -playerRotationSpeed * Time.fixedDeltaTime);
                }
            }
            _rigidbody.MoveRotation(_rigidbody.rotation + targetAngle);
        }

        private float CalculateViewAtAngle()
        {
            Vector2 position = transform.position;
            float angleFromRight = CalculateAngleFromRight(position, _lookAtTarget);
            float angleToForward = CalculateAngleFromRight(position, position + (Vector2)transform.right);

            float targetAngle = angleToForward - angleFromRight;
            if (targetAngle > 180)
            {
                targetAngle = targetAngle - 360;
            }

            return targetAngle;
        }

        private static float CalculateAngleFromRight(Vector2 originPoint, Vector2 targetPoint)
        {
            Vector2 direction = targetPoint - originPoint;
            return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }

        private Vector2 GetLookAtPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Assert(Camera.main != null, "Camera.main != null");
            mousePosition.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}