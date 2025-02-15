using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle7Player : MonoBehaviour
{
    public float baseSpeed = 3f;  // �⺻ �̵� �ӵ� (�ڵ� �̵�)
    public float boostSpeed = 6f; // DŰ �Ǵ� �� ����Ű �Է� �� �ӵ� ����
    public float moveSpeed = 3f;  // �����¿� �̵� �ӵ�
    public PlayerHealth health;

    public Animator rocketAnimator;
    public Rigidbody2D rb;
    public float fallSpeed = 5f; // ���� �ӵ�
    public bool falling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (falling)           //�������� ���̸� �̵� x
            return;

        float moveX = Input.GetAxis("Horizontal"); // �¿� �̵� �Է�
        float moveY = Input.GetAxis("Vertical");   // ���� �̵� �Է�

        // DŰ �Ǵ� �� ����Ű�� ������ �ӵ� ����, �ƴϸ� �⺻ �ӵ� ����
        float currentSpeed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? boostSpeed : baseSpeed;

        // �⺻������ ���������� �̵� (DŰ �Ǵ� �� ����Ű �Է� �� �� ������)
        Vector3 moveDirection = new Vector3(1, 0, 0) * currentSpeed * Time.deltaTime;

        // �����¿� �̵� (�ӵ��� ����)
        moveDirection += new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

        // �̵� ����
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
        rocketAnimator.SetTrigger("Fall"); // �ִϸ��̼� ����
        rb.velocity = new Vector2(0, -fallSpeed); // �Ʒ��� ����
    }
}
