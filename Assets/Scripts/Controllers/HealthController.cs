using System;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        [Min(0)][SerializeField] private float maxHealthPoints;

        private float _currentHealthPoints;

        private void Awake()
        {
            _currentHealthPoints = maxHealthPoints;
        }

        private void Update()
        {
            if (_currentHealthPoints < 0)
            {
                // The thing dies.
            }
        }

        public void DealDamage(float damage)
        {
            _currentHealthPoints -= maxHealthPoints;
        }
    }
}