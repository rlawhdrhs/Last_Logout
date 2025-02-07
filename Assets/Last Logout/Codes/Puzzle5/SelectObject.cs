using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public GameObject Darken;
    private Color originalColor;
    private bool isOnObject = false;
    public bool isSelected = false; // 이 오브젝트의 색상이 어두워졌는지 여부

    void Update()
    {
        // Spacebar가 눌렸고, 오브젝트 위에 있으면
        if (isOnObject && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isSelected)
            {
                // 색상을 어두워지게 변경
                Darken.SetActive(true);
                isSelected = true; // 어두워짐 상태 기록
            }
            else
            {
                // 색상을 원래대로 되돌리기
                Darken.SetActive(false);
                isSelected = false; // 원래 상태로 되돌림
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 캐릭터와 충돌했을 때
        {
            isOnObject = true;
        }
    }
    // 캐릭터가 오브젝트와 충돌했을 때
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 캐릭터가 오브젝트에서 벗어났을 때
        {
            isOnObject = false;
        }
    }
}

