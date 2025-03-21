using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailInteraction : MonoBehaviour
{
    public GameObject mailOpenObject; // 클릭 시 활성화할 오브젝트 (열린 메일)
    public SpriteRenderer whiteOutSprite; // 화이트 아웃 효과를 줄 스프라이트
    public float fadeSpeed = 1.5f; // 페이드 속도
    public EndingManager endingManager;
    private float alpha = 0f;
    private bool isInputBlocked = true; // 입력 차단 변수
    public PlaySound sound;
    void Start()
    {
        StartCoroutine(StopSec()); // 씬이 시작되면 2초 후에 입력 활성화
    }

    void Update()
    {
        if (isInputBlocked) return; // 입력 차단
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sound.StopSound();
            mailOpenObject.SetActive(true); // 열린 메일 활성화
            StartCoroutine(WhiteOutEffect()); // 화이트 아웃 효과 실행
        }
    }
    IEnumerator StopSec()
    {
        yield return new WaitForSeconds(2f);
        isInputBlocked = false; // 2초 후 입력 허용
    }
    IEnumerator WhiteOutEffect()
    {
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            whiteOutSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        endingManager.LoadEnding(endingManager.endingNumber);
    }
}

