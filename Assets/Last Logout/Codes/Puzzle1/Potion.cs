using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healAmount = 1; // ȸ����
    public float floatSpeed = 1f; // ���ٴϴ� �ӵ�
    public float floatHeight = 0.2f; // ���ٴϴ� ����

    private Vector3 startPos; // ���� ��ġ
    private float timeOffset; // ������ ���� ����

    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, 2f); // ������ �ð� ���̸� �༭ ���� ���� ���� Ÿ�ֿ̹� �������� �ʰ� ��
    }

    void Update()
    {
        // Sin �Լ��� �̿��� �ε巴�� ���ٴϴ� ���
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatHeight, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject); // ���� ����
            }
        }
    }
}
