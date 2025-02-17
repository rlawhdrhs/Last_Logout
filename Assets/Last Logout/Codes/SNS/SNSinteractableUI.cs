using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSinteractableUI : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject feedit; // 이동할 UI 오브젝트
    public GameObject ScrollCursor;
    public SNSinteractableUI counter;
    public int moveCount = 0; // 이동 횟수
    public const int maxMoveCount = 3; // 최대 이동 가능 횟수
    public float moveAmount = 100f; // 한 번 이동하는 거리 (UI 단위)
    public float moveSpeed = 5f; // 이동 속도
    private bool isMoving = false; // 현재 이동 중인지 확인

    SpriteRenderer sprite;
    Color originColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originColor = sprite.color;
    }


    void Update()
    {
        moveCount = counter.moveCount;
        UpdateScrollCursor();
        if (isPlayerNearby && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                string portalName = gameObject.name; // 현재 포탈 오브젝트 이름 가져오기

                switch (portalName)
                {
                    case "Up":
                        MoveUI(-1); // 위로 이동
                        break;
                    case "Down":
                        MoveUI(1); // 아래로 이동
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            sprite.color = new Color(0, 0, 0, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            sprite.color = originColor;
        }
    }
    void UpdateScrollCursor()
    {
        float amount = (float)moveCount / (float)maxMoveCount * 4.5f;
        ScrollCursor.transform.position = new Vector3(8.9f, 2f - amount, 0);
    }
    void MoveUI(int direction)
    {
        if (direction == 1 && moveCount < maxMoveCount) // 위로 이동 (이전 이동 기록이 있어야 가능)
        {
            moveCount++;
            StartCoroutine(MoveSmoothly(feedit.transform.position + new Vector3(0, moveAmount, 0)));
        }
        else if (direction == -1 && moveCount > 0) // 아래로 이동 (최대 4번까지 가능)
        {
            moveCount--;
            StartCoroutine(MoveSmoothly(feedit.transform.position - new Vector3(0, moveAmount, 0)));
        }
    }

    IEnumerator MoveSmoothly(Vector3 targetPos)
    {
        isMoving = true;
        float elapsedTime = 0f;
        float duration = 0.3f; // 이동 시간
        Vector3 startPos = feedit.transform.position;

        while (elapsedTime < duration)
        {
            feedit.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        feedit.transform.position = targetPos; // 정확한 위치 보정
        isMoving = false;
    }
}
