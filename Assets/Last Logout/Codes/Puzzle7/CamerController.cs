using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    public float moveSpeed = 3f; // 카메라 이동 속도
    public int maxXpos;
    void Update()
    {
        if (transform.position.x >= maxXpos)
            return;
        // 카메라를 오른쪽으로 이동
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }
}
