using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class PoolManager : MonoBehaviour
    {
        // Public members
        public static PoolManager instance;

        // Private members
        // Cache last pool index where found last game object
        int lastIndex;

        private void Awake()
        {
            instance = this;
            lastIndex = 0;
        }

        public Pool GeneratePool(string poolName, GameObject prefab, int size)
        {
            Pool newPool = new Pool();
            newPool.poolName = poolName;
            newPool.prefab = prefab;
            newPool.poolList = new List<GameObject>();
            for(int i = 0; i < size; i++)
            {
                newPool.poolList.Add(Instantiate<GameObject>(newPool.prefab, Vector3.one, Quaternion.identity));
                newPool.poolList[i].SetActive(false);
            }
            return newPool;
        }

        public GameObject GetObjectFromPool(Pool pool)
        {
            for(int i = lastIndex; i < pool.listSize; i++)
            {
                // Check if index exceed list size. If so, go back to zero
                if (i + 1 >= pool.listSize)
                    lastIndex = 0;
                if (!pool.poolList[i].activeInHierarchy)
                    return pool.poolList[i];
            }
            // If all gameobjects on pool are active, create a new one
            GameObject newObj = Instantiate<GameObject>(pool.prefab, Vector3.one, Quaternion.identity);
            newObj.SetActive(false);
            pool.poolList.Add(newObj);
            return newObj;
        }
    }
}

