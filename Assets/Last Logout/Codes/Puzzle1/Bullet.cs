using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f; // 총알이 존재하는 최대 시간
    public float damage;
    public int bulletDamage = 1; // 총알이 적에게 줄 피해량

    void Start()
    {
        // lifeTime 이후 총알 삭제
        Destroy(gameObject, lifeTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage); // 적 체력 감소
            }

            Destroy(gameObject); // 총알 제거
        }
        else if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // 총알 제거
        }
    }
    void OnBecameInvisible()
    {
        // 화면 밖으로 나가면 총알 삭제
        Destroy(gameObject);
    }
}
