using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Utility;

namespace Managers
{
    public class OrganismsSpawnmanager : MonoBehaviour
    {
        [Header("Numbers of organisms")] [SerializeField] [Range(0, 1000)]
        int goodGuysNum;

        [SerializeField] [Range(0, 1000)] int badGuysNum;

        [Header("Game start: organisms burst")] [SerializeField] [Range(0, 1000)]
        int goodGuysBurst;

        [SerializeField] [Range(0, 1000)] int badGuysBurst;

        [Header("Organisms prefabs")] [SerializeField]
        GameObject goodGuy;

        [SerializeField] GameObject badGuy;

        [Header("Organisms Pools")] [SerializeField]
        Pool goodGuysPool;

        [SerializeField] Pool badGuysPool;


        [Header("Borders")] [SerializeField] bool useBorderTransform;
        [SerializeField] float borderX;
        [SerializeField] float borderY;

        [Header("Borders float")] [SerializeField]
        float borderXfloat;

        [SerializeField] float borderYfloat;

        [Header("Borders transform")] [SerializeField]
        Transform borderXTrans;

        [SerializeField] Transform borderYTrans;

        [Header("Spawn time")] [SerializeField]
        float timeToSpawn;

        [SerializeField] float timeToSpawnMin;
        [SerializeField] float timeToSpawnMax;

        bool _canSpawn;

        private void Awake()
        {
            if (useBorderTransform)
            {
                borderX = borderXTrans.position.x;
                borderY = borderYTrans.position.y;
            }
            else
            {
                borderX = borderXfloat;
                borderY = borderYfloat;
            }
        }

        private void Start()
        {
            _canSpawn = true;
            goodGuysPool = PoolManager.instance.GeneratePool("GoodGuys", goodGuy, goodGuysNum);
            badGuysPool = PoolManager.instance.GeneratePool("BadGuys", badGuy, badGuysNum);
            InitOrganisms();
        }

        private void Update()
        {
            if (goodGuysPool != null && badGuysPool != null && _canSpawn)
            {
                StartCoroutine(SpawnOrganisms());
            }
        }

        void InitOrganisms()
        {
            for (int i = 0; i < goodGuysBurst; i++)
            {
                SpawnFromPool(goodGuysPool);
            }

            for (int i = 0; i < badGuysBurst; i++)
            {
                SpawnFromPool(badGuysPool);
            }
        }

        IEnumerator SpawnOrganisms()
        {
            _canSpawn = false;
            // Spawn good guy
            timeToSpawn = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawn);
            SpawnFromPool(goodGuysPool);

            // Spawn bad guy
            timeToSpawn = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawn);
            SpawnFromPool(badGuysPool);

            _canSpawn = true;
        }

        void SpawnFromPool(Pool poolToSpawnFrom)
        {
            var spawnedObject = PoolManager.instance.GetObjectFromPool(poolToSpawnFrom);
            var spawnedObjectSprite = spawnedObject.GetComponent<SpriteRenderer>();
            var spawnedObjectSpriteBounds = spawnedObjectSprite.bounds;
            
            Vector3 targetPosition =new Vector3(
                Random.Range((borderX - (spawnedObjectSpriteBounds.size.x / 2)),
                    (-borderX + (spawnedObjectSpriteBounds.size.x / 2))),
                Random.Range((borderY - (spawnedObjectSprite.bounds.size.y / 2)),
                    (-borderY + (spawnedObjectSpriteBounds.size.y / 2))));

            OrganismRandomMovementController movementController = spawnedObject.GetComponent<OrganismRandomMovementController>();
            if (movementController != null)
            {
                movementController.TeleportOrganism(targetPosition);
            }
            else
            {
                spawnedObject.transform.position = targetPosition;
            }
            
            spawnedObject.SetActive(true);
        }
    }
}