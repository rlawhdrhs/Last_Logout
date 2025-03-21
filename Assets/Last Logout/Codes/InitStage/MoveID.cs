using System.Collections;
using UnityEngine;

public class MoveID : MonoBehaviour
{
    public float speed = 1f;  // �̵� �ӵ�
    public RectTransform[] waypoints; // ������ �̵� ��ǥ�� (UI ���)
    private int currentIndex = 0; // ���� ��ǥ ��ǥ
    private RectTransform rectTransform;
    public PlaySound sound;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // TMP Text�� RectTransform ��������

        if (waypoints.Length > 0)
            rectTransform.anchoredPosition = waypoints[0].anchoredPosition; // ���� ��ġ ����
        if (waypoints.Length > 0)
            transform.position = waypoints[0].position; // ���� ��ġ
        StartCoroutine(MoveLoop());
        // GameManager ���� üũ
        if (GameManager.instance == null || (!GameManager.instance.PuzzleClear[1] && !GameManager.instance.PuzzleClear[3]) || GameManager.instance.PuzzleClear[6] == true)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            Vector2 startPos = rectTransform.anchoredPosition;
            Vector2 targetPos = waypoints[currentIndex].anchoredPosition;

            float time = 0;
            while (time < 1f)
            {
                time += Time.deltaTime * speed;
                rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, time); // �ε巴�� �̵�
                yield return null;
            }
            sound.Play();
            yield return new WaitForSeconds(0.1f); // ��� ����ٰ� �̵� (ƨ��� ���� �߰�)

            currentIndex = (currentIndex + 1) % waypoints.Length; // ���� ��ǥ �������� ����
        }
    }
}
