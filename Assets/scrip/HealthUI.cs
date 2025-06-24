using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
   [SerializeField] private Text healthText;
    private void Start()
    {
        Player.Instance.OnHealthChanged += UpdateHealthUI;
        Player.Instance.OnPlayerDied += ShowGameOver;
        UpdateHealthUI(Player.Instance.Health);
    }
    private void OnDestroy()
    {
       if( Player.Instance != null)
        {
            Player.Instance.OnHealthChanged -= UpdateHealthUI;
            Player.Instance.OnPlayerDied -= ShowGameOver;
        }
    }

    private void UpdateHealthUI(int health)
    {
        
        if(healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
        else
        {
            Debug.LogWarning("Health Text is not assigned in the HealthUI script.");
        }
    }

    private void ShowGameOver()
    {
        healthText.text = "Game Over!";
        // Optionally, you can disable player controls or show a game over screen here.
    }

    
}
