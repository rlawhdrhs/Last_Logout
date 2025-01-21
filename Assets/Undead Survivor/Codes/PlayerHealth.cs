using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;                 // 최대 체력
    public int currentHealth;                 // 현재 체력
    public GameObject heartContainer;         // 하트 컨테이너 (부모 오브젝트)

    public Sprite fullHeartSprite;            // 채워진 하트 Sprite
    public Sprite emptyHeartSprite;           // 빈 하트 Sprite

    private SpriteRenderer[] hearts;          // 하트 Sprite Renderer 배열
    private SpriteRenderer playerSprite;      // 플레이어의 Sprite Renderer
    private bool isTakingDamage = false;      // 피격 애니메이션 중복 방지
    private bool isInvincible = false;        // 무적 상태 여부
    public float knockbackForce = 5f;         // 넉백 힘 (적에게 맞았을 때 뒤로 밀리는 힘)

    private Rigidbody2D rb;                   // 플레이어의 Rigidbody2D

    void Start()
    {
        // 현재 체력을 최대 체력으로 초기화
        currentHealth = maxHealth;

        // 하트 Sprite Renderer 배열 가져오기
        hearts = heartContainer.GetComponentsInChildren<SpriteRenderer>();

        // 플레이어의 SpriteRenderer 가져오기
        playerSprite = GetComponent<SpriteRenderer>();

        // Rigidbody2D 가져오기
        rb = GetComponent<Rigidbody2D>();

        // 하트 UI 초기화
        UpdateHealthUI();
    }

    public void TakeDamage(int damage, GameObject enemy)
    {
        if (isInvincible) return; // 무적 상태일 때는 피해를 받지 않음

        // 체력 감소
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // 하트 UI 업데이트
        UpdateHealthUI();

        // 넉백 효과 적용
        if (enemy != null)
        {
            Vector2 knockbackDirection = (transform.position - enemy.transform.position).normalized; // 적과 반대 방향
            rb.velocity = knockbackDirection * knockbackForce;
        }

        // 맞았을 때 무적 & 피격 애니메이션 실행
        StartCoroutine(InvincibilityCoroutine(enemy));

        // 체력이 0이 되면 게임 종료 처리
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    IEnumerator InvincibilityCoroutine(GameObject enemy)
    {
        isInvincible = true;  // 무적 상태 시작
        isTakingDamage = true;

        // 적과 충돌 무시
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
        }

        Color originalColor = playerSprite.color; // 원래 색상 저장
        playerSprite.color = Color.red; // 맞았을 때 빨간색으로 변경

        yield return new WaitForSeconds(0.1f);
        playerSprite.color = originalColor;

        // 깜빡이는 효과 (3번 깜빡임)
        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        isTakingDamage = false;
        playerSprite.enabled = true; // 원래대로 복구
        isInvincible = false; // 무적 상태 해제

        // 적과 충돌 다시 활성화
        if (enemyCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentHealth) ? fullHeartSprite : emptyHeartSprite;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverScene");
    }
}
