using UnityEngine;
using System.Collections.Generic;
public class SpawnAndTurn : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Tốc độ xoay

    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
      
        GameObject spawnedObject = ObjectManager.Instance.GetObject();

       
        spawnedObject.transform.position = Vector3.zero;

        spawnedObject.transform.rotation = Quaternion.identity;
        StartCoroutine(RotateObject(spawnedObject));
    }

    System.Collections.IEnumerator RotateObject(GameObject obj)
    {
        while (obj.activeSelf) 
        {
            obj.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    void OnDisable()
    {
        GameObject[] activeObjects = GameObject.FindGameObjectsWithTag("YourTag"); // Thay "YourTag" bằng tag của prefab
        foreach (GameObject obj in activeObjects)
        {
            ObjectManager.Instance.ReturnObject(obj);
        }
    }
}