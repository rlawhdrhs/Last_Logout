using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle7Player : MonoBehaviour
{
    public float baseSpeed = 3f;  // 기본 이동 속도 (자동 이동)
    public float boostSpeed = 6f; // D키 또는 → 방향키 입력 시 속도 증가
    public float moveSpeed = 3f;  // 상하좌우 이동 속도
    public PlayerHealth health;

    public Animator rocketAnimator;
    public Rigidbody2D rb;
    public float fallSpeed = 5f; // 낙하 속도
    public bool falling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (falling)           //떨어지는 중이면 이동 x
            return;

        float moveX = Input.GetAxis("Horizontal"); // 좌우 이동 입력
        float moveY = Input.GetAxis("Vertical");   // 상하 이동 입력

        // D키 또는 → 방향키를 누르면 속도 증가, 아니면 기본 속도 유지
        float currentSpeed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? boostSpeed : baseSpeed;

        // 기본적으로 오른쪽으로 이동 (D키 또는 → 방향키 입력 시 더 빠르게)
        Vector3 moveDirection = new Vector3(1, 0, 0) * currentSpeed * Time.deltaTime;

        // 상하좌우 이동 (속도는 일정)
        moveDirection += new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // 이동 적용
        transform.position += moveDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !health.isInvincible)
        {
            health.TakeDamage(1, null);
        }
    }

    public void TriggerFall()
    {
        falling = true;
        rocketAnimator.SetTrigger("Fall"); // 애니메이션 실행
        rb.velocity = new Vector2(0, -fallSpeed); // 아래로 낙하
    }
}
