using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;       // �̵� �ӵ�
    public int health = 3;        // �� ü��
    public int damage = 1;        // �÷��̾�� ���� ���ط�
    private Transform player;     // �÷��̾� ��ġ ����

    void Start()
    {
        // �÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // �÷��̾ ���� �̵�
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        // ü�� ����
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // ü���� 0 ���ϰ� �Ǹ� ����
        }
    }
}
