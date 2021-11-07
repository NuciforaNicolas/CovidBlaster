using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [System.Serializable]
    public class Pool
    {
        public string poolName;
        public List<GameObject> poolList;
        public GameObject prefab;
        public int listSize;
    }
}

