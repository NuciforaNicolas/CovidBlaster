using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private float rateOfFire;
        [SerializeField] private GameObject fastProjectilePrefab;

        private Transform _worldTransform;
        private float _timeOfLastShot;

        private void Awake()
        {
            _worldTransform = GameObject.Find("GameWorld").transform;
            _timeOfLastShot = Time.time;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (_timeOfLastShot + (1 / rateOfFire) < Time.time)
                {
                    FireProjectile();
                    _timeOfLastShot = Time.time;
                }
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