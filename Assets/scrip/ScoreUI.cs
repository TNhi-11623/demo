using UnityEngine;
using System.Collections.Generic;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager; // Assuming you have a ScoreManager to manage scores
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    private void OnEnable()
    {
        scoreManager.OnScoreChanged.AddListener(UpdateScoreText);
    }
    private void OnDisable()
    {
        scoreManager.OnScoreChanged.RemoveListener(UpdateScoreText);
    }
    private void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore.ToString();
    }
}
