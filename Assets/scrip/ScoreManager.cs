using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    private int score = 0;
    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged.Invoke(score);
        Debug.Log("Score updated: " + score);
    }
}
