using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score { get; private set; }
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    instance = singleton.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null && instance != this)
        {
            Destroy(gameObject);
            return;

        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    public void AddScore(int score)
    {
        Score += score;
        Debug.Log("Score: " + Score);
    }
    
}
