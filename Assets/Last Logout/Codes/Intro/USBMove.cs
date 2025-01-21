using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class USBMove : MonoBehaviour
{
    public Transform targetPosition; // USB�� ������ ��ġ (��ǻ�� ��Ʈ)
    public float moveDuration = 2f;  // �̵� �ð� (��)

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

            // ���� ���� (Lerp)���� �ε巴�� �̵�
            transform.position = Vector3.Lerp(startPosition, targetPosition.position, t);

            // �̵� �Ϸ� �� ���߱�
            if (t >= 1f)
            {
                isMoving = false;
                Invoke("StartLoadingBar", 0.5f); // USB ���� �� �ε� �� ����
            }
        }
    }

    void StartLoadingBar()
    {
        FindObjectOfType<LoadingBar>().StartLoading();
    }
}
