using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanGoal : MonoBehaviour
{
    private SokobanGameManager gameManager;
    public PlaySound goal;
    void Start()
    {
        gameManager = FindObjectOfType<SokobanGameManager>(); // GameManager 찾기
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("File")) // 파일이 목표에 도착
        {
            goal.Play();
            Debug.Log("파일이 휴지통에 도착!");
            StartCoroutine(CheckStageClear());
        }
    }
    IEnumerator CheckStageClear()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("🎯 스테이지 클리어! 다음 스테이지로 이동!");
        gameManager.LoadNextStage();
    }
}
