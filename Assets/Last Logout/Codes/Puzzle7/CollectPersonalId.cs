using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPersonalId : MonoBehaviour
{
    public float floatSpeed = 1f; // ���ٴϴ� �ӵ�
    public float floatHeight = 0.2f; // ���ٴϴ� ����
    private float timeOffset; // ������ ���� ����
    public Puzzle7Manager manager;
    public bool isCollected = false;
    public PlaySound sound;
    void Start()
    {
        timeOffset = Random.Range(0f, 2f); // ������ �ð� ���̸� �༭ ���� ���� ���� Ÿ�ֿ̹� �������� �ʰ� ��
    }

    void Update()
    {
        // Sin �Լ��� �̿��� �ε巴�� ���ٴϴ� ���
        transform.position = transform.position + new Vector3(0, Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatHeight, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCollected)
        {
            gameObject.SetActive(false); // �߰� �浹 ����
            sound.Play();
            isCollected = true;
            manager.collectPI += 1;
            Destroy(gameObject);
        }
    }
}
