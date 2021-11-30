using Enums.Sound;
using FMODUnity;
using UnityEditor.Build.Content;
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
        private GameUiController _gameUiController;

        private void Awake()
        {
            _currentHealthPoints = maxHealthPoints;
            _gameUiController = GameObject.Find("Canvas").GetComponent<GameUiController>();
        }

        private void Update()
        {
            if (_currentHealthPoints <= 0)
            {
                _currentHealthPoints = maxHealthPoints;
                if (UnitName.Virus.Equals(unitName))
                {
                    RuntimeManager.PlayOneShot("event:/Sounds/KillGreen", transform.position);
                    _gameUiController.AddKill();
                }
                else if (UnitName.RedCell.Equals(unitName))
                {
                    RuntimeManager.PlayOneShot("event:/Sounds/KillRedAndLoseLife", transform.position);
                    _gameUiController.SubtractPatientHealth();
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