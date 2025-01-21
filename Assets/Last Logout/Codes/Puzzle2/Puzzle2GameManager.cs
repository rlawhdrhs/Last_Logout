﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Puzzle2GameManager : MonoBehaviour
{
    public float timeLimit = 10f; // 5분 제한 시간 (초 단위)
    public float currentTime;

    public Text timerText; // UI 타이머
    public Text stageText; // 스테이지 표시

    public int currentQuiz = 1; // 현재 스테이지
    public int totalQuiz = 3; // 총 스테이지 수

    public SpriteRenderer[] hearts; // 하트 UI 배열 (3개)
    public Sprite fullHeart; // 꽉 찬 하트 스프라이트
    public Sprite emptyHeart; // 빈 하트 스프라이트

    private int life = 3; // 초기 체력 3개

    void Start()
    {
        currentTime = timeLimit;
        UpdateUI();
    }

    void Update()
    {
        // 타이머 업데이트
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime) + "";
        stageText.text = currentQuiz + " / " + totalQuiz;
    }

    public void ReduceLife()
    {
        if (life <= 0) return; // 체력이 0이면 실행 X

        life--; // 체력 감소
        hearts[life].sprite = emptyHeart; // 해당 하트를 빈 하트로 변경

        if (life <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("⏳ 제한 시간 종료! 게임 오버!");
        SceneManager.LoadScene("GameOverScene"); // 게임 오버 씬으로 이동
    }

    public void LoadNextQuiz()
    {
        if (currentQuiz < totalQuiz)
        {
            currentQuiz++;
        }
        else
        {
            GameClear();
        }
    }

    public void GameClear()
    {
        Debug.Log("🎉 모든 스테이지 클리어!");
        SceneManager.LoadScene("GameClearScene"); // 게임 클리어 씬으로 이동
    }
}
