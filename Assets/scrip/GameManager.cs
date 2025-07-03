using UnityEngine;
using MyGame.Spawning;
public class GameManager : MonoBehaviour
{
     [SerializeField] public int SpawnObjectCountInASecond = 1;
    [SerializeField] private string pooledObjectId = "Cube";
    private float spawnTimer = 0f;
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 1f) // Spawn every 1 second
        {
            spawnTimer -= 1f; // Reset the timer
            for(var i = 0; i < SpawnObjectCountInASecond; i++) 
            {
                SpawnObject.Instance.Spawn(pooledObjectId);
            }
        }
    }

  
}
