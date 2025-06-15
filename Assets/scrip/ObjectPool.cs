using UnityEngine;
using System.Collections.Generic;
using System;
using Utils;
public class ObjectPool : MonoSingleton<ObjectPool>
{
    public static ObjectPool Instance { get; private set; }

    [System.Serializable]
    public struct PoolObject
    {
        public string name;
        public GameObject prefab;
        public int intialcount;
    }

    public PoolObject[] objectstoPool;
    private Dictionary<string, Queue<GameObject>> objectPool;

    public void Start()
    {
        if (objectPool == null)
        {
            objectPool = new Dictionary<string, Queue<GameObject>>();
        }
        Init();
    }

    public void Init()
    {
        if (objectstoPool == null)
        {
            objectstoPool = new PoolObject[0];
        }
        foreach (var poolObject in objectstoPool)
        {
            if (!objectPool.ContainsKey(poolObject.name))
            {
                objectPool.Add(poolObject.name, new Queue<GameObject>());
            }
            for (var i = 0; i < poolObject.intialcount; i++)
            {
                var obj = Instantiate(poolObject.prefab, transform); // FIXED LINE
                obj.SetActive(false);
                objectPool[poolObject.name].Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (objectPool.ContainsKey(name) && objectPool[name].Count > 0)
        {
            var obj = objectPool[name].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        var poolObject = Array.Find(objectstoPool, x => x.name == name);
        var outPut = Instantiate(poolObject.prefab, transform);
        outPut.SetActive(true);
        Debug.LogWarning($"No object available in pool: {name}");
        return outPut;
    }

    public void ReturnObject(string name, GameObject obj)
    {
        if (objectPool.ContainsKey(name))
        {
            obj.SetActive(false);
            objectPool[name].Enqueue(obj);
        }
        else
       
        {
            objectPool.Add(name, new Queue<GameObject>());
            obj.SetActive(false);
            objectPool[name].Enqueue(obj);
            Debug.LogWarning($"Attempting to return object to non-existent pool: {name}");
            Destroy(obj);
        }
    }
}