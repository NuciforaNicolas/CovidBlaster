using UnityEngine;

namespace Controllers
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private float rateOfFire, bigProjectileReloadTime;
        [SerializeField] private GameObject fastProjectilePrefab, bigProjectilePrefab;

        private Transform _worldTransform;
        private float _timeOfLastShotFast, _timeOfLastShotBig;

        private void Awake()
        {
            _worldTransform = GameObject.FindGameObjectWithTag("GameWorld").transform;
            _timeOfLastShotFast = Time.time;
            _timeOfLastShotBig = Time.time;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (_timeOfLastShotFast + (1 / rateOfFire) < Time.time)
                {
                    FireFastProjectile();
                    _timeOfLastShotFast = Time.time;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (_timeOfLastShotBig + bigProjectileReloadTime < Time.time)
                {
                    FireBigProjectile();
                    _timeOfLastShotBig = Time.time;
                }
            }
        }

        public void FireFastProjectile()
        {
            Transform fastProjectileSpawnParentTransform = transform.Find("ProjectileSpawnPoints/FastProjectile");
            for (int spawnIter = 0; spawnIter < fastProjectileSpawnParentTransform.childCount; spawnIter++)
            {
                Transform spawnParentTransform = fastProjectileSpawnParentTransform.GetChild(spawnIter);
                Instantiate(fastProjectilePrefab, spawnParentTransform.position, spawnParentTransform.rotation);
            }
        }

        public void FireBigProjectile()
        {
            Transform fastProjectileSpawnParentTransform = transform.Find("ProjectileSpawnPoints/BigProjectile");
            for (int spawnIter = 0; spawnIter < fastProjectileSpawnParentTransform.childCount; spawnIter++)
            {
                Transform spawnParentTransform = fastProjectileSpawnParentTransform.GetChild(spawnIter);
                Instantiate(bigProjectilePrefab, spawnParentTransform.position, spawnParentTransform.rotation);
            }
        }
    }
}