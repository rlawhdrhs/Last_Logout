using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f; // �Ѿ��� �����ϴ� �ִ� �ð�
    public float damage;
    public int bulletDamage = 1; // �Ѿ��� ������ �� ���ط�

    void Start()
    {
        // lifeTime ���� �Ѿ� ����
        Destroy(gameObject, lifeTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage); // �� ü�� ����
            }

            Destroy(gameObject); // �Ѿ� ����
        }
        else if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // �Ѿ� ����
        }
    }
    void OnBecameInvisible()
    {
        // ȭ�� ������ ������ �Ѿ� ����
        Destroy(gameObject);
    }
}
