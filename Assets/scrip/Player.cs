using UnityEngine;
using System;
public class Player : MonoBehaviour
{ 
    private Rigidbody rb;
    public static Player Instance { get; private set; }
    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 100;
    public event Action<int> OnHealthChanged;
    public event Action OnPlayerDied;
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, 100);
            OnHealthChanged?.Invoke(health);
            if (health <= 0)
            {
                OnPlayerDied?.Invoke();
                // Destroy(gameObject); 
            }
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = new Vector3(moveX, moveY, 0);
        Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);
        if (Input.GetKeyDown(KeyCode.H))
        {
            Health -= 10; // Example damage
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // GameObject bullet = BulletPool.Instance.GetBullet();
            // bullet.transform.position = transform.position;
            GameObject bullet = BulletPool.Instance.GetBullet(transform.position);

        }
    }
    

}
