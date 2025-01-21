using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform player;         // ĳ���� Transform
    public Vector2 offset; // ĳ���� ���� �� ��ġ ������

    private Vector2 lastDirection = Vector2.right; // ĳ������ ������ �ٶ󺸴� ����

    void Update()
    {
        // �Է� ���� Ȯ��
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputDirection != Vector2.zero)
        {
            lastDirection = inputDirection.normalized; // ������ ���� ������Ʈ
        }

        // �� ��ġ ������Ʈ
        transform.position = (Vector2)player.position + lastDirection * offset.magnitude;

        // �� ȸ�� ������Ʈ
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

        // ���� ������ �� ���� ������ �ʵ��� ����
        if (lastDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(180f, 0f, -angle); // ������ ����
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
