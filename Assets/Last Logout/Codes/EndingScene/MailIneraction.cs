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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 마우스 왼쪽 버튼 클릭 감지
        {
            mailOpenObject.SetActive(true); // 열린 메일 활성화
            StartCoroutine(WhiteOutEffect()); // 화이트 아웃 효과 실행
        }
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

