using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Dùng cho FirstOrDefault
using System;     // Dùng cho Convert.ChangeType nếu cần

namespace SpawnObject
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        private Dictionary<int, List<GameObject>> _pool = new();

        public void InitializePool(GameObject prefab, int size)
        {
            int key = prefab.GetInstanceID();

            if (!_pool.ContainsKey(key))
            {
                _pool[key] = new List<GameObject>();
            }

            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                _pool[key].Add(obj);
            }
        }

        public GameObject GetObject(GameObject prefab)
        {
            int key = prefab.GetInstanceID();

            if (_pool.ContainsKey(key))
            {
                GameObject inactiveObj = _pool[key].FirstOrDefault(o => !o.activeInHierarchy);
                if (inactiveObj != null)
                {
                    inactiveObj.SetActive(true);
                    return inactiveObj;
                }
            }

            // Nếu không có đối tượng nào sẵn, tạo mới
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(true);

            if (!_pool.ContainsKey(key))
            {
                _pool[key] = new List<GameObject>();
            }

            _pool[key].Add(newObj);
            return newObj;
        }
        public void ReturnAllObjects()
        {
            foreach (var list in _pool.Values)
            {
                foreach (var obj in list)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
