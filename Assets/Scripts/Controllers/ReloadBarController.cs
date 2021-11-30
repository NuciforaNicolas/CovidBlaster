using System;
using UnityEngine;

namespace Controllers
{
    public class ReloadBarController : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private PlayerWeaponController _weaponController;

        private float _reloadBarWidth, _previousReloadPercentage;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _weaponController = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>();
            _reloadBarWidth = _rectTransform.rect.width;
        }

        private void Update()
        {
            float currentReloadPercentage = _weaponController.GetBigProjectileReloadPercentage();
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                currentReloadPercentage * _reloadBarWidth);
            if (currentReloadPercentage >= 0.99999f && _previousReloadPercentage < 0.99999f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/2ndWpReady");
            } else if (currentReloadPercentage <= 0.1f && _previousReloadPercentage >= 0.99999f)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/2ndWpReload");
            }

            _previousReloadPercentage = currentReloadPercentage;
        }
    }
}