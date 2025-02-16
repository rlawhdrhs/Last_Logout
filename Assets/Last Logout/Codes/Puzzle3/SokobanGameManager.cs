using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SokobanGameManager : MonoBehaviour
{
    public float timeLimit = 300f; // 5분 제한 시간 (초 단위)
    private float currentTime;
    private int min, sec, time;

    public Text timerText; // UI 타이머
    public Text stageText; // 스테이지 표시

    public int currentStage = 1; // 현재 스테이지
    public int totalStages = 4; // 총 스테이지 수

    void Start()
    {
        // PlayerPrefs에서 이전 타이머 값 불러오기
        if (PlayerPrefs.HasKey("currentTime") && currentStage != 1)
        {
            currentTime = PlayerPrefs.GetFloat("currentTime");
        }
        else
        {
            currentTime = timeLimit; // 새로운 게임 시작 시 초기화
        }

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

        // 리셋키 (스페이스바)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartStage();
        }
    }

    void RestartStage()
    {
        PlayerPrefs.SetFloat("currentTime", currentTime); // 현재 타이머 값 저장
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    void UpdateUI()
    {
        time = Mathf.CeilToInt(currentTime);
        min = time / 60;
        sec = time % 60;
        timerText.text = min + ":" + sec;
        stageText.text = currentStage + " / " + totalStages;
    }

    void GameOver()
    {
        Debug.Log("⏳ 제한 시간 종료! 게임 오버!");
        PlayerPrefs.SetFloat("currentTime", timeLimit);
        SceneManager.LoadScene("GameOverScene"); // 게임 오버 씬으로 이동
    }

    public void LoadNextStage()
    {
        if (currentStage < totalStages)
        {
            currentStage++;
            PlayerPrefs.SetFloat("currentTime", currentTime); // 타이머 값 저장
            SceneManager.LoadScene("Puzzle3 Stage" + currentStage); // 다음 스테이지 로드
        }
        else
        {
            GameClear();
        }
    }

    void GameClear()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetPuzzleCleared(2); // 클리어 상태 저장
        }
        Debug.Log("🎉 모든 스테이지 클리어!");
        SceneManager.LoadScene("GameClearScene"); // 게임 클리어 씬으로 이동
    }
}
