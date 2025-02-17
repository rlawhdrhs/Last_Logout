using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNSinteractableUI : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject feedit; // �̵��� UI ������Ʈ
    public GameObject ScrollCursor;
    public SNSinteractableUI counter;
    public int moveCount = 0; // �̵� Ƚ��
    public const int maxMoveCount = 3; // �ִ� �̵� ���� Ƚ��
    public float moveAmount = 100f; // �� �� �̵��ϴ� �Ÿ� (UI ����)
    public float moveSpeed = 5f; // �̵� �ӵ�
    private bool isMoving = false; // ���� �̵� ������ Ȯ��

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
                string portalName = gameObject.name; // ���� ��Ż ������Ʈ �̸� ��������

                switch (portalName)
                {
                    case "Up":
                        MoveUI(-1); // ���� �̵�
                        break;
                    case "Down":
                        MoveUI(1); // �Ʒ��� �̵�
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
        if (direction == 1 && moveCount < maxMoveCount) // ���� �̵� (���� �̵� ����� �־�� ����)
        {
            moveCount++;
            StartCoroutine(MoveSmoothly(feedit.transform.position + new Vector3(0, moveAmount, 0)));
        }
        else if (direction == -1 && moveCount > 0) // �Ʒ��� �̵� (�ִ� 4������ ����)
        {
            moveCount--;
            StartCoroutine(MoveSmoothly(feedit.transform.position - new Vector3(0, moveAmount, 0)));
        }
    }

    IEnumerator MoveSmoothly(Vector3 targetPos)
    {
        isMoving = true;
        float elapsedTime = 0f;
        float duration = 0.3f; // �̵� �ð�
        Vector3 startPos = feedit.transform.position;

        while (elapsedTime < duration)
        {
            feedit.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        feedit.transform.position = targetPos; // ��Ȯ�� ��ġ ����
        isMoving = false;
    }
}
