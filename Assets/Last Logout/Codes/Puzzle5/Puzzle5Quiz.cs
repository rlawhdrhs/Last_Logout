using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5Quiz : MonoBehaviour
{
    public float moveDistance = 9f;  // 이동 거리
    public float moveSpeed = 5f;     // 이동 속도
    public float shakeAmount = 0.1f; // 흔들림 정도
    public float shakeDuration = 1f; // 흔들리는 시간
    public Puzzle5Manager puzzlemanager;

    public int problemNum; // 문제 번호
    private Vector3 originalPosition; // 원래 위치
    private bool isShaking = false;
    private bool isMoving = false;
    private Vector3 targetPosition; // 목표 위치
    public bool movable = false; // 이동 가능 여부
    public bool correctAnswer = false; // 정답 여부

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // 목표 위치로 부드럽게 이동
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // 목표 위치에 도달하면 이동 종료
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
        else
        {
            UpdateQuizPosition();
        }
    }

    // 현재 문제와 다음 문제의 위치 조정
    void UpdateQuizPosition()
    {
        if (puzzlemanager.currentQuiz == problemNum)
        {
            transform.position = new Vector3(0, -0.2f, 0);
        }
        else if (puzzlemanager.currentQuiz == problemNum - 1)
        {
            transform.position = new Vector3(0, -0.1f, 0);
        }
    }

    // 정답 체크 후 이동 or 흔들림
    public void CheckAnswer(bool playerChoice)
    {
        if (!movable) return;

        if (playerChoice == correctAnswer)
        {
            Move(playerChoice);
            movable = false;
        }
        else
        {
            StartCoroutine(ShakeEffect());
            puzzlemanager.ReduceLife();
        }
    }

    // 정답일 경우 이동
    void Move(bool isLeft)
    {
        float direction = isLeft ? -1 : 1;
        targetPosition = transform.position + new Vector3(moveDistance * direction, 0, 0);
        isMoving = true;
        originalPosition = targetPosition;
    }

    // 흔들림 효과
    IEnumerator ShakeEffect()
    {
        if (isShaking) yield break;
        isShaking = true;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 원래 위치로 복구
        transform.position = originalPosition;
        isShaking = false;
    }
}
