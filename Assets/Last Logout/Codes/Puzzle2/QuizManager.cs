using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject[] questions; // 문제 오브젝트 배열 (1+1=? 같은 문제)
    public SpriteRenderer[] choiceSprites; // 선택지 SpriteRenderer 배열 (2,3,4)

    public int[] correctAnswers; // 정답 인덱스 (예: [0, 2, 1] 등)
    public Sprite wrongSprite;  // 틀렸을 때 바뀔 스프라이트

    public Puzzle2GameManager GameManager;

    private int currentQuestion = 0;
    private bool isAnswering = false;

    void Start()
    {
        ShowQuestion(currentQuestion);
    }

    public void SelectChoice(int selectedIndex)
    {
        if (isAnswering) return;
        isAnswering = true;

        if (selectedIndex == correctAnswers[currentQuestion])
        {
            NextQuestion();
        }
        else
        {
            StartCoroutine(WrongAnswerEffect(selectedIndex));
        }
    }

    private IEnumerator WrongAnswerEffect(int index)
    {
        GameManager.ReduceLife(); // 틀리면 체력 감소
        Sprite temp = choiceSprites[index].sprite;
        choiceSprites[index].sprite = wrongSprite;
        yield return new WaitForSeconds(0.5f);
        choiceSprites[index].sprite = temp;
        isAnswering = false; // 다시 선택 가능하도록 변경
    }

    private void NextQuestion()
    {
        currentQuestion++;
        GameManager.currentQuiz++;
        if (currentQuestion >= questions.Length)
        {
            Debug.Log("퀴즈 완료!"); // 추후 클리어 화면으로 이동 추가
            GameManager.GameClear();
        }
        else
        {
            ShowQuestion(currentQuestion);
        }

        isAnswering = false;
    }

    private void ShowQuestion(int index)
    {
        GameManager.currentTime = 10f;
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(i == index);
        }
    }
}
