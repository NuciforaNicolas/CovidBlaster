using System;
using UnityEngine;

namespace Controllers
{
    public class ReloadBarController : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private PlayerWeaponController _weaponController;

        private float _reloadBarWidth;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _weaponController = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>();
            _reloadBarWidth = _rectTransform.rect.width;
        }

        private void Update()
        {
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                _weaponController.GetBigProjectileReloadPercentage() * _reloadBarWidth);
        }
    }
}