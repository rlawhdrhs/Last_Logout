using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;                 // �ִ� ü��
    public int currentHealth;                 // ���� ü��
    public GameObject heartContainer;         // ��Ʈ �����̳� (�θ� ������Ʈ)

    public Sprite fullHeartSprite;            // ä���� ��Ʈ Sprite
    public Sprite emptyHeartSprite;           // �� ��Ʈ Sprite

    private SpriteRenderer[] hearts;          // ��Ʈ Sprite Renderer �迭
    private SpriteRenderer playerSprite;      // �÷��̾��� Sprite Renderer
    public bool isInvincible = false;        // ���� ���� ����
    public float knockbackForce = 5f;         // �˹� �� (������ �¾��� �� �ڷ� �и��� ��)
    private Rigidbody2D rb;                   // �÷��̾��� Rigidbody2D

    public PlaySound reduceHP;
    public PlaySound heal;
    void Start()
    {
        // ���� ü���� �ִ� ü������ �ʱ�ȭ
        currentHealth = maxHealth;

        // ��Ʈ Sprite Renderer �迭 ��������
        hearts = heartContainer.GetComponentsInChildren<SpriteRenderer>();

        // �÷��̾��� SpriteRenderer ��������
        playerSprite = GetComponent<SpriteRenderer>();

        // Rigidbody2D ��������
        rb = GetComponent<Rigidbody2D>();

        // ��Ʈ UI �ʱ�ȭ
        UpdateHealthUI();
        GameObject g = GameObject.Find("reduceHP");
        reduceHP = g.GetComponent<PlaySound>();
    }

    public void TakeDamage(int damage, GameObject enemy)
    {
        if (isInvincible) return; // ���� ������ ���� ���ظ� ���� ����

        // ü�� ����
        reduceHP.Play();
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // ��Ʈ UI ������Ʈ
        UpdateHealthUI();

        // �˹� ȿ�� ����
        if (enemy != null)
        {
            Vector2 knockbackDirection = (transform.position - enemy.transform.position).normalized; // ���� �ݴ� ����
            rb.velocity = knockbackDirection * knockbackForce;

            // �¾��� �� ���� & �ǰ� �ִϸ��̼� ����
            StartCoroutine(InvincibilityCoroutine(enemy));
        }
        else
        {
            StartCoroutine(Puzzle7InvincibilityEffect());
        }

        // ü���� 0�� �Ǹ� ���� ���� ó��
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    IEnumerator InvincibilityCoroutine(GameObject enemy)
    {
        isInvincible = true;  // ���� ���� ����

        // ���� �浹 ����
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
        }

        Color originalColor = playerSprite.color; // ���� ���� ����
        playerSprite.color = Color.red; // �¾��� �� ���������� ����

        yield return new WaitForSeconds(0.1f);
        playerSprite.color = originalColor;

        // �����̴� ȿ�� (3�� ������)
        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.enabled = true; // ������� ����
        isInvincible = false; // ���� ���� ����

        // ���� �浹 �ٽ� Ȱ��ȭ
        if (enemyCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
        }
    }
    IEnumerator Puzzle7InvincibilityEffect()
    {
        isInvincible = true;
        playerSprite.color = Color.red; // �浹 �� ������ ����

        // �����̱� ȿ��
        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.color = Color.white; // ���� ���� ����
        isInvincible = false;
    }
    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentHealth) ? fullHeartSprite : emptyHeartSprite;
        }
    }

    public void Heal(int amount)
    {
        if (heal != null)
            heal.Play();
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void GameOver()
    {
        if (GameManager.instance != null) 
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "Puzzle1 Stage1")
            {
                GameManager.instance.PuzzleFail[0] = true;
            }
            else if(sceneName == "Puzzle7 Stage1")
            {
                GameManager.instance.PuzzleFail[6] = true;
            }
        }
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverScene");
    }
}
