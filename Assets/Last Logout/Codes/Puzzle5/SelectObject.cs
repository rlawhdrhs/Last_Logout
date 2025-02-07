using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject Darken;
    private Color originalColor;
    private bool isOnObject = false;
    public bool isSelected = false; // �� ������Ʈ�� ������ ��ο������� ����

    void Update()
    {
        // Spacebar�� ���Ȱ�, ������Ʈ ���� ������
        if (isOnObject && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSelected)
            {
                // ������ ��ο����� ����
                Darken.SetActive(true);
                isSelected = true; // ��ο��� ���� ���
            }
            else
            {
                // ������ ������� �ǵ�����
                Darken.SetActive(false);
                isSelected = false; // ���� ���·� �ǵ���
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ĳ���Ϳ� �浹���� ��
        {
            isOnObject = true;
        }
    }
    // ĳ���Ͱ� ������Ʈ�� �浹���� ��
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ĳ���Ͱ� ������Ʈ���� ����� ��
        {
            isOnObject = false;
        }
    }
}

