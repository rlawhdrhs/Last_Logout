using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject[] questions; // ���� ������Ʈ �迭 (1+1=? ���� ����)
    public SpriteRenderer[] choiceSprites; // ������ SpriteRenderer �迭 (2,3,4)

    public int[] correctAnswers; // ���� �ε��� (��: [0, 2, 1] ��)
    public Sprite wrongSprite;  // Ʋ���� �� �ٲ� ��������Ʈ

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
        GameManager.ReduceLife(); // Ʋ���� ü�� ����
        Sprite temp = choiceSprites[index].sprite;
        choiceSprites[index].sprite = wrongSprite;
        yield return new WaitForSeconds(0.5f);
        choiceSprites[index].sprite = temp;
        isAnswering = false; // �ٽ� ���� �����ϵ��� ����
    }

    private void NextQuestion()
    {
        currentQuestion++;
        GameManager.currentQuiz++;
        if (currentQuestion >= questions.Length)
        {
            Debug.Log("���� �Ϸ�!"); // ���� Ŭ���� ȭ������ �̵� �߰�
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
