using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event Action<int> OnHealthChanged;    // Add this event
    public event Action OnPlayerDied;            // Add this event

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            if (value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
            OnHealthChanged?.Invoke(health); // Fire event

            if (health == 0)
            {
                OnPlayerDied?.Invoke(); // Fire event when dead
            }

            // If you still want to notify HealthService, keep this line:
            // HealthService.Instance.NotifyHealthChanged(health);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
