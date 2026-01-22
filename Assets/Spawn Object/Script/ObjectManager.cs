using UnityEngine;
using System.Collections.Generic;
public class ObjectManager : MonoBehaviour
{
    
    public static ObjectManager Instance { get; private set; }

   
    private readonly Queue<GameObject> objectPool = new Queue<GameObject>();
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int poolSize = 5;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabToPool, Vector3.zero, Quaternion.identity);
            obj.SetActive(false); // Tắt object ban đầu
            objectPool.Enqueue(obj);
        }
    }


    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(prefabToPool, Vector3.zero, Quaternion.identity);
            return newObj;
        }
    }

    
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}