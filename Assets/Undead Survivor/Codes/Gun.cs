using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform player;         // 캐릭터 Transform
    public Vector2 offset; // 캐릭터 기준 총 위치 오프셋

    private Vector2 lastDirection = Vector2.right; // 캐릭터의 마지막 바라보는 방향

    void Update()
    {
        // 입력 방향 확인
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputDirection != Vector2.zero)
        {
            lastDirection = inputDirection.normalized; // 마지막 방향 업데이트
        }

        // 총 위치 업데이트
        transform.position = (Vector2)player.position + lastDirection * offset.magnitude;

        // 총 회전 업데이트
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

        // 왼쪽 방향일 때 총을 뒤집지 않도록 설정
        if (lastDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(180f, 0f, -angle); // 뒤집기 방지
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
