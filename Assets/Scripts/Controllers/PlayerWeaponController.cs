using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;

        private Transform _worldTransform;

        private void Awake()
        {
            _worldTransform = GameObject.Find("GameWorld").transform;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FireProjectile();
            }
        }

        public void FireProjectile()
        {
            Transform playerTransform;
            Instantiate(projectilePrefab, (playerTransform = transform).Find("ProjectileSpawnPoint").position, playerTransform.rotation,
                _worldTransform);
        }
    }
}