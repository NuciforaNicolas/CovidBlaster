using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private float rateOfFire;
        [SerializeField] private GameObject fastProjectilePrefab;

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
            Transform fastProjectileSpawnParentTransform = transform.Find("ProjectileSpawnPoints/FastProjectile");
            for (int spawnIter = 0; spawnIter < fastProjectileSpawnParentTransform.childCount; spawnIter++)
            {
                Transform spawnParentTransform = fastProjectileSpawnParentTransform.GetChild(spawnIter);
                Instantiate(fastProjectilePrefab, spawnParentTransform.position, spawnParentTransform.rotation);
            }
            // Instantiate(fastProjectilePrefab, fastProjectileSpawnParentTransform.Find("ProjectileSpawnPoint").position, fastProjectileSpawnParentTransform.rotation,
            //     _worldTransform);
        }
    }
}