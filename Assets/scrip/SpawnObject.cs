using UnityEngine;
using System.Collections;
using System;
using Utils; // Cần để kế thừa MonoSingleton

namespace MyGame.Spawning
{
    public class SpawnObject : MonoSingleton<SpawnObject>
    {
        public static SpawnObject Instance => MonoSingleton<SpawnObject>.Instance;

        private ObjectPool objectPool;
        private string poolObjectName;
        private float currentX = 0;

        public void Start()
        {
            objectPool = ObjectPool.Instance;
        }

        public void Spawn(string poolObjectName)
        {
            this.poolObjectName = poolObjectName;
            var go = objectPool.GetObject(poolObjectName);
            if (go != null)
            {
                go.transform.position = new Vector3(currentX, 0, 0);
                go.transform.rotation = Quaternion.identity;
                currentX += 1;
                StartCoroutine(ReturnToPool(go));
            }
            else
            {
                Debug.LogWarning($"Failed to spawn object: {poolObjectName}");
            }
        }

        private IEnumerator ReturnToPool(GameObject go)
        {
            yield return new WaitForSeconds(1f);
            objectPool.ReturnObject(poolObjectName, go);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Object"))
            {
                Spawn(poolObjectName);
            }
        }
    }
}
