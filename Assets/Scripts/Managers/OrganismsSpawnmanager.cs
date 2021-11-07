using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Managers
{
    public class OrganismsSpawnmanager : MonoBehaviour
    {
        [Header("Numbers of organisms")]

        [SerializeField]
        [Range(0, 1000)]
        int goodGuysNum;
        [SerializeField]
        [Range(0, 1000)]
        int badGuysNum;

        [Header("Game start: organisms burst")]
        [SerializeField]
        [Range(0, 1000)]
        int goodGuysBurst;
        [SerializeField]
        [Range(0, 1000)]
        int badGuysBurst;

        [Header("Organisms prefabs")]
        [SerializeField]
        GameObject goodGuy;
        [SerializeField]
        GameObject badGuy;

        [Header("Organisms Pools")]
        [SerializeField]
        Pool goodGuysPool;
        [SerializeField]
        Pool badGuysPool;


        [Header("Borders")]
        [SerializeField]
        bool useBorderTransform;
        [SerializeField]
        float borderX;
        [SerializeField]
        float borderY;

        [Header("Borders float")]
        [SerializeField]
        float borderXfloat;
        [SerializeField]
        float borderYfloat;

        [Header("Borders transform")]
        [SerializeField]
        Transform borderXTrans;
        [SerializeField]
        Transform borderYTrans;

        [Header("Spawn time")]
        [SerializeField]
        float timeToSpawn;
        [SerializeField]
        float timeToSpawnMin;
        [SerializeField]
        float timeToSpawnMax;

        bool canSpawn;

        private void Awake()
        {
            if(useBorderTransform)
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
            canSpawn = true;
            goodGuysPool = PoolManager.instance.GeneratePool("GoodGuys", goodGuy, goodGuysNum);
            badGuysPool = PoolManager.instance.GeneratePool("BadGuys", badGuy, badGuysNum);
            InitOrganisms();
        }

        private void Update()
        {
            if(goodGuysPool != null && badGuysPool != null && canSpawn)
            {
                StartCoroutine(SpawnOrganisms());
            }
        }

        void InitOrganisms()
        {
            for(int i = 0; i < goodGuysBurst; i++)
            {
                SpawnGoodGuy();
            }
            for(int i = 0; i < badGuysBurst; i++)
            {
                SpawnBadGuy();
            }
        }

        IEnumerator SpawnOrganisms()
        {
            canSpawn = false;
            // Spawn good guy
            timeToSpawn = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawn);
            SpawnGoodGuy();

            // Spawn bad guy
            timeToSpawn = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawn);
            SpawnBadGuy();

            canSpawn = true;
        }

        void SpawnGoodGuy()
        {
            var goodGuy = PoolManager.instance.GetObjectFromPool(goodGuysPool);
            var goodGuySprite = goodGuy.GetComponent<SpriteRenderer>();
            goodGuy.transform.position = new Vector3(Random.Range((borderX - (goodGuySprite.bounds.size.x / 2)), (-borderX + (goodGuySprite.bounds.size.x / 2))), Random.Range((borderY - (goodGuySprite.bounds.size.y / 2)), (-borderY + (goodGuySprite.bounds.size.y / 2))));
            goodGuy.SetActive(true);
        }

        void SpawnBadGuy()
        {
            var badGuy = PoolManager.instance.GetObjectFromPool(badGuysPool);
            var badGuySprite = badGuy.GetComponent<SpriteRenderer>();
            badGuy.transform.position = new Vector3(Random.Range((borderX - (badGuySprite.bounds.size.x / 2)), (-borderX + (badGuySprite.bounds.size.x / 2))), Random.Range((borderY - (badGuySprite.bounds.size.y / 2)), (-borderY + (badGuySprite.bounds.size.y / 2))));
            badGuy.SetActive(true);
        }
    }
}