using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzle5Manager : MonoBehaviour
{
    public Puzzle5Quiz[] puzzle5Quizs = new Puzzle5Quiz[4];
    public int currentQuiz = 0;
    public float timeLimit = 10f; // 제한 시간 (초)
    public float currentTime;
    public Text timerText;
    public GameObject timer;

    public SpriteRenderer[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private int life = 3;

    private bool openSelectPuzzle = false;
    private bool canInput = true;  // ⬅ 키 입력 가능 여부 추가
    public float cooldownTime = 0.5f; // ⬅ 입력 쿨타임 (초)

    void Awake()
    {
        // 같은 타입의 오브젝트가 있는지 확인
        Puzzle5Manager existingManager = FindObjectOfType<Puzzle5Manager>();

        if (existingManager != null && existingManager != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        puzzle5Quizs[currentQuiz].movable = true;
        currentTime = timeLimit;
        UpdateHeartsUI();
    }

    void Update()
    {
        if (openSelectPuzzle)
            return;
        CheckQuiz();
        HandleInput();  // ⬅ 모든 입력을 여기서 처리
        if (timerText == null)
            return;
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            currentTime = timeLimit;
            ReduceLife();
        }
    }

    void HandleInput()
    {
        if (!canInput) return;  // ⬅ 입력 쿨타임 중이면 무시

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(HandleInputCooldown(true));  // ⬅ '예' 입력 처리
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(HandleInputCooldown(false)); // ⬅ '아니요' 입력 처리
        }
    }

    IEnumerator HandleInputCooldown(bool playerChoice)
    {
        canInput = false;  // ⬅ 입력 막기
        puzzle5Quizs[currentQuiz].CheckAnswer(playerChoice);  // ⬅ 현재 문제에만 적용
        yield return new WaitForSeconds(cooldownTime);
        canInput = true;  // ⬅ 다시 입력 가능하게 변경
    }

    void CheckQuiz()
    {
        if (!puzzle5Quizs[currentQuiz].movable)
        {
            if (currentQuiz < puzzle5Quizs.Length - 1)
            {
                if ((puzzle5Quizs[currentQuiz].correctAnswer))
                {
                    StartCoroutine(WaitNextScene("Puzzle5 Stage2"));
                    openSelectPuzzle = true;
                    timer.SetActive(false);
                }
                currentQuiz++;
                puzzle5Quizs[currentQuiz].movable = true;
                currentTime = timeLimit;
            }
            else
            {
                StartCoroutine(WaitNextScene("GameClearScene"));
            }
        }
    }

    void UpdateUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime) + "";
    }

    public void ReduceLife()
    {
        if (life <= 0) return;
        life--;
        UpdateHeartsUI();

        if (life <= 0)
        {
            StartCoroutine(WaitNextScene("GameOverScene"));
        }
    }
    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // 체력 값에 맞게 하트를 업데이트
            if (i < life)
            {
                hearts[i].sprite = fullHeart;  // 살아있는 하트
            }
            else
            {
                hearts[i].sprite = emptyHeart;  // 빈 하트
            }
        }
    }

    void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOverScene");
    }

    public void GameClear()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameClearScene");
        if (GameManager.instance != null)
            GameManager.instance.PuzzleClear[4] = true;
    }

    public void QuizClear()
    {
        openSelectPuzzle = false;
        SceneManager.LoadScene("Puzzle5 Stage1");
        for (int i = 0; i < puzzle5Quizs.Length; i++)
        {
            puzzle5Quizs[i].gameObject.SetActive(true);
        }
        timer.SetActive(true);
    }
    IEnumerator WaitNextScene(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        if (sceneName == "GameClearScene")
            GameClear();
        else if (sceneName == "GameOverScene")
            GameOver();
        else
        {
            for (int i = 0; i < puzzle5Quizs.Length; i++)
            {
                puzzle5Quizs[i].gameObject.SetActive(false);
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}