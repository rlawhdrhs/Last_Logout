using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    public float moveSpeed = 3f; // ī�޶� �̵� �ӵ�
    public int maxXpos;
    void Update()
    {
        if (transform.position.x >= maxXpos)
            return;
        // ī�޶� ���������� �̵�
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }
}
