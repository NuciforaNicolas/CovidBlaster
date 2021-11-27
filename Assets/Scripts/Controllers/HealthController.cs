using Enums.Sound;
using FMODUnity;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        [Min(0)] [SerializeField]
        private float maxHealthPoints;

        [SerializeField]
        private UnitName unitName;

        private float _currentHealthPoints;

        private void Awake()
        {
            _currentHealthPoints = maxHealthPoints;
        }

        private void Update()
        {
            if (_currentHealthPoints <= 0)
            {
                _currentHealthPoints = maxHealthPoints;
                if (UnitName.Virus.Equals(unitName))
                {
                    RuntimeManager.PlayOneShot("event:/Sounds/KillGreen", transform.position);
                }
                else if (UnitName.RedCell.Equals(unitName))
                {
                    RuntimeManager.PlayOneShot("event:/Sounds/KillRedAndLoseLife", transform.position);
                }

                gameObject.SetActive(false);
            }
        }

        public void DealDamage(float damage)
        {
            _currentHealthPoints -= damage;
            if (UnitName.Virus.Equals(unitName))
            {
                RuntimeManager.PlayOneShot("event:/Sounds/ShootGreen", transform.position);
            }
            else if (UnitName.RedCell.Equals(unitName))
            {
                RuntimeManager.PlayOneShot("event:/Sounds/ShootRed", transform.position);
            }
        }
    }
}