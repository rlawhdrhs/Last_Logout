using System.Collections;
using UnityEngine;

public class MoveID : MonoBehaviour
{
    public float speed = 1f;  // 이동 속도
    public RectTransform[] waypoints; // 고정된 이동 좌표들 (UI 요소)
    private int currentIndex = 0; // 현재 목표 좌표
    private RectTransform rectTransform;
    public PlaySound sound;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // TMP Text의 RectTransform 가져오기

        if (waypoints.Length > 0)
            rectTransform.anchoredPosition = waypoints[0].anchoredPosition; // 시작 위치 설정
        if (waypoints.Length > 0)
            transform.position = waypoints[0].position; // 시작 위치
        StartCoroutine(MoveLoop());
        // GameManager 조건 체크
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
                rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, time); // 부드럽게 이동
                yield return null;
            }
            sound.Play();
            yield return new WaitForSeconds(0.1f); // 잠깐 멈췄다가 이동 (튕기는 느낌 추가)

            currentIndex = (currentIndex + 1) % waypoints.Length; // 다음 목표 지점으로 변경
        }
    }
}
