using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [System.Serializable]
    public class Pool
    {
        public string mName;
        public List<GameObject> mList;
        public GameObject mPrefab;
        public int mSize;
        public int mLastIndex;
    }
}

