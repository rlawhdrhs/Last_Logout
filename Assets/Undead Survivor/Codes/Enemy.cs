using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;       // 이동 속도
    public int health = 3;        // 적 체력
    public int damage = 1;        // 플레이어에게 가할 피해량
    private Transform player;     // 플레이어 위치 참조

    void Start()
    {
        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어를 향해 이동
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        // 체력 감소
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject); // 체력이 0 이하가 되면 제거
        }
    }
}
