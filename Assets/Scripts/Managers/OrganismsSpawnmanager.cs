using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Managers
{
    public class OrganismsSpawnmanager : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 1000)]
        int goodGuysNum, badGuysNum;

        [SerializeField]
        GameObject goodGuy, badGuy;

        [SerializeField]
        Pool goodGuysPool, badGuysPool;

        [SerializeField]
        float timeToSpawn, timeToSpawnMin, timeToSpawnMax;

        // To-Do: to remove. Debug only
        [SerializeField]
        Transform player;

        bool canSpawn;

        private void Start()
        {
            canSpawn = true;
            goodGuysPool = PoolManager.instance.GeneratePool("GoodGuys", goodGuy, goodGuysNum);
            badGuysPool = PoolManager.instance.GeneratePool("BadGuys", badGuy, badGuysNum);
        }

        private void Update()
        {
            if(goodGuysPool != null && badGuysPool != null && canSpawn)
            {
                StartCoroutine(SpawnOrganisms());
            }
        }

        IEnumerator SpawnOrganisms()
        {
            canSpawn = false;
            // Spawn good guy
            timeToSpawn = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawn);
            var goodGuy = PoolManager.instance.GetObjectFromPool(goodGuysPool);
            goodGuy.transform.position = new Vector3(Random.Range(player.position.x, Random.Range(-10, 10)), Random.Range(player.position.y, Random.Range(-10, 10)));
            goodGuy.SetActive(true);

            // Spawn bad guy
            timeToSpawn = Random.Range(timeToSpawnMin, timeToSpawnMax);
            yield return new WaitForSeconds(timeToSpawn);
            var badGuy = PoolManager.instance.GetObjectFromPool(badGuysPool);
            badGuy.transform.position = new Vector3(Random.Range(player.position.x, Random.Range(-10, 10)), Random.Range(player.position.y, Random.Range(-10, 10)));
            badGuy.SetActive(true);

            canSpawn = true;
        }
    }
}