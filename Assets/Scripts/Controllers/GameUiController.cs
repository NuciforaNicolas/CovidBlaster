using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class GameUiController : MonoBehaviour
    {
        [SerializeField]
        private int patientMaxHealth;

        private TextMeshProUGUI _killsText;
        private RectTransform _healthBarRectTransform;
        private float _healthBarStartingWidth;

        private int _numberOfKills, _patientHealthPoints;

        private void Awake()
        {
            _killsText = transform.Find("TopPanel/ScorePanel/KillsText").GetComponent<TextMeshProUGUI>();
            _healthBarRectTransform = transform.Find("TopPanel/HealthPanel/HealthBar").GetComponent<RectTransform>();
            _healthBarStartingWidth = _healthBarRectTransform.rect.width;
            _numberOfKills = 0;
            _patientHealthPoints = patientMaxHealth;
        }

        public void AddKill()
        {
            _numberOfKills += 1;
            _killsText.text = "Kills: " + _numberOfKills;
        }

        public void SubtractPatientHealth()
        {
            _patientHealthPoints -= 1;
            float patientHealthPercentage = (float) _patientHealthPoints / patientMaxHealth;
            _healthBarRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                _healthBarStartingWidth * patientHealthPercentage);
        }
    }
}