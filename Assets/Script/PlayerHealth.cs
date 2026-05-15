using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vidas")]
    [SerializeField] int maxLives = 3;
    int currentLives;

    [Header("Invencibilidad temporal")]
    [SerializeField] float invincibleDuration = 1.5f;
    bool isInvincible = false;
    float invincibleTimer = 0f;

    [Header("Feedback visual")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkInterval = 0.15f;
    float blinkTimer = 0f;

    // Evento igual que CoinManager.OnAddPoints
    public static event Action OnLivesChanged;

    // Singleton para que LivesText pueda leer el valor
    public static PlayerHealth Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLives = maxLives;
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        OnLivesChanged?.Invoke(); // actualiza UI al inicio
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0f)
            {
                blinkTimer = blinkInterval;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }

            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentLives--;
        Debug.Log("Vida quitada. Vidas restantes: " + currentLives);
        OnLivesChanged?.Invoke(); // avisa a la UI

        if (currentLives <= 0)
            Die();
        else
        {
            isInvincible = true;
            invincibleTimer = invincibleDuration;
            blinkTimer = blinkInterval;
        }
    }

    public void AddLife()
    {
        currentLives++; 
        Debug.Log("¡Vida extra! Vidas: " + currentLives);
        OnLivesChanged?.Invoke();
    }

    void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CoinManager.OnBonusLife += AddLife;
    }

    private void OnDisable()
    {
        CoinManager.OnBonusLife -= AddLife;
    }

    public int GetCurrentLives() => currentLives;
    public int GetMaxLives() => maxLives;
}