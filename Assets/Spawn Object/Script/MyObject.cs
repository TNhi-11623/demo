using UnityEngine;

namespace SpawnObject
{
    public class MyObject : MonoBehaviour
    {
        [SerializeField] private int _objectID;
        public int ObjectID => _objectID;

        [SerializeField] private int _objectSize = 10;

        private bool m_isInitialized = false;

        public void Spawn(Vector3 pos, Quaternion rot)
        {
            InitializePool();
            var go = ObjectPool.Instance.GetObject(gameObject);
            go.transform.SetPositionAndRotation(pos, rot);
        }

        private void InitializePool()
        {
            if (m_isInitialized)
                return;

            m_isInitialized = true;
            ObjectPool.Instance.InitializePool(gameObject, _objectSize);
        }
    }
}
