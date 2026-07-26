using UnityEngine;
using System.Collections; 
using UnityEngine.UI;

public class PlayerHealthPoint : MonoBehaviour
{
    [Header("Health Settings")]
    public float hp = 3.0f;
    public float damage1 = 1.0f;

    [Header("Invincibility Settings")]
    public float invincibilityTime = 1.0f; // 데미지 후 무적 시간
    public float blinkInterval = 0.1f;    // 깜빡이는 속도
    public Sprite die;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    [Header("UI Elements")]
    public GameObject gameOverPanel; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameOverPanel.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌
    {
        
        if (collision.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        hp -= damage1;
        Debug.Log("Player HealthPoint: " + hp);

        if (hp <= 0)
        {
            Die();
        }
        else
        {
            // 피격 상태
            StartCoroutine(OnDamageRoutine());
        }
    }

    IEnumerator OnDamageRoutine()
    {
        isInvincible = true;

        float timer = 0f;
        while (timer < invincibilityTime)
        {
            SetAlpha(0.2f);
            yield return new WaitForSeconds(blinkInterval);

            SetAlpha(1.0f);
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2f;
        }

        SetAlpha(1.0f);
        isInvincible = false;
    }

    void SetAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");
        spriteRenderer.sprite = die;

        GetComponent<PlayerMove>().enabled = false;

        EnemySpawner[] spawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.GameOver();
        }

        WaveMove[] waves = FindObjectsByType<WaveMove>(FindObjectsSortMode.None);
        foreach (WaveMove wave in waves)
        {
            wave.GameOver();
        }

        StartCoroutine(GameOverPanel());
    }

    IEnumerator GameOverPanel()
    {
        yield return new WaitForSeconds(2);
        gameOverPanel.SetActive(true);
    }
}
