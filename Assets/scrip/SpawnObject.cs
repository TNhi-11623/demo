using UnityEngine;
using System.Collections;
using Utils;
using System;
public class SpawnObject : MonoSingleton<SpawnObject>
{
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
            go.transform.position = new Vector3(currentX, 0, 0); // Set position to zero or any desired position
            go.transform.rotation = Quaternion.identity; // Reset rotation
            currentX += 1; // Increment the X position for the next object
            // Invoke("ReturnToPool", go, 1f);
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
