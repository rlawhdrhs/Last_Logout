using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle5Quiz : MonoBehaviour
{
    public float moveDistance = 9f;
    public float moveSpeed = 1f;
    public float shakeAmount = 0.1f;
    public float shakeDuration = 1f;
    public Puzzle5Manager puzzlemanager;

    public int problemNum;
    private Vector3 originalPosition;
    private bool isShaking = false;
    public bool isMoving = false;
    private Vector3 targetPosition;
    public bool movable = false;
    public bool correctAnswer = false;

    void Start()
    {

        originalPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

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

    void Move(bool isLeft)
    {
        float direction = isLeft ? -1 : 1;
        targetPosition = transform.position + new Vector3(moveDistance * direction, 0, 0);
        isMoving = true;
    }

    IEnumerator ShakeEffect()
    {
        if (isShaking) yield break;
        isShaking = true;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPosition + new Vector3(offsetX, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;
    }
}