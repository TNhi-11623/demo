using UnityEngine;
using System.Collections.Generic;
public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> bulletPool = new List<GameObject>();

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
            return;
        }
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public GameObject GetBullet(Vector3 position)
{
    foreach (GameObject bullet in bulletPool)
    {
        if (!bullet.activeInHierarchy)
        {
            bullet.transform.position = position;
            bullet.SetActive(true);
            return bullet;
        }
    }

    GameObject newBullet = Instantiate(bulletPrefab, position, Quaternion.identity);
    bulletPool.Add(newBullet);
    return newBullet;
}


    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
       
    }

}
