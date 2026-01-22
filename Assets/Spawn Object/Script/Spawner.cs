using UnityEngine;
namespace SpawnObject
{
    public sealed class Spawner : MonoBehaviour
    {
        public class MyObject : MonoBehaviour
        {
            public void Spawn(Vector3 position, Quaternion rotation)
            {
                transform.position = position;
                transform.rotation = rotation;
                gameObject.SetActive(true);
            }

            public void ReturnToPool()
            {
                gameObject.SetActive(false);
            }
        }
    }
}
