using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class GameUiController : MonoBehaviour
    {
        private TextMeshProUGUI _killsText;
        private RectTransform _healthBarRectTransform;
        
        

        private void Awake()
        {
            _killsText = transform.Find("TopPanel/ScorePanel/KillsText").GetComponent<TextMeshProUGUI>();
            _healthBarRectTransform = transform.Find("TopPanel/HealthPanel/HealthBar").GetComponent<RectTransform>();
        }
    }
}