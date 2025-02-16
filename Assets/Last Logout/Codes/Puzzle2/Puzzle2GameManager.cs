using System.Collections;
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

    public int currentQuiz = 1;
    public int totalQuiz = 4; 

    public SpriteRenderer[] hearts; // 하트 UI 배열 (3개)
    public Sprite fullHeart; // 꽉 찬 하트 스프라이트
    public Sprite emptyHeart; // 빈 하트 스프라이트
    private string textcolor;
    private string color;
    private string red = "FF7F7F";
    private string blue = "A1E5FF";
    private string purple = "DF90FF";
    private string green = "83FF7E";

    private int life = 3; // 초기 체력 3개

    void Start()
    {
        currentTime = timeLimit;
        UpdateUI();
    }

    void Update()
    {
        // 타이머 업데이트
        if (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            ReduceLife();
            currentTime = timeLimit;
        }
    }

    void UpdateUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime) + "";
        if (currentQuiz == 1)
        {
            textcolor = red;
            color = "빨간색";
        }
        else if (currentQuiz == 2)
        {
            textcolor = green;
            color = "초록색";
        }
        else if (currentQuiz == 3)
        {
            textcolor = purple;
            color = "보라색";
        }
        else
        {
            textcolor = blue;
            color = "파란색";
        }

        stageText.text = "※ <color=#"+textcolor+">"+color+"</color> 보안 문자를 올바르게 입력하세요. ("+currentQuiz + " / " + totalQuiz+")";
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
        if (GameManager.instance != null)
            GameManager.instance.PuzzleClear[1] = true;
        SceneManager.LoadScene("GameClearScene"); // 게임 클리어 씬으로 이동
    }
}
