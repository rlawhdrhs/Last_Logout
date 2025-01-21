using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class USBMove : MonoBehaviour
{
    public Transform targetPosition; // USB가 도착할 위치 (컴퓨터 포트)
    public float moveDuration = 2f;  // 이동 시간 (초)

    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isMoving = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // 선형 보간 (Lerp)으로 부드럽게 이동
            transform.position = Vector3.Lerp(startPosition, targetPosition.position, t);

            // 이동 완료 후 멈추기
            if (t >= 1f)
            {
                isMoving = false;
                Invoke("StartLoadingBar", 0.5f); // USB 꽂힌 후 로딩 바 시작
            }
        }
    }

    void StartLoadingBar()
    {
        FindObjectOfType<LoadingBar>().StartLoading();
    }
}
