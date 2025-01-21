using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanGoal : MonoBehaviour
{
    private SokobanGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<SokobanGameManager>(); // GameManager 찾기
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("File")) // 파일이 목표에 도착
        {
            Debug.Log("파일이 휴지통에 도착!");
            CheckStageClear();
        }
    }

    void CheckStageClear()
    {
        Debug.Log("🎯 스테이지 클리어! 다음 스테이지로 이동!");
        gameManager.LoadNextStage();
    }
}
