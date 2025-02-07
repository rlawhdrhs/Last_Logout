using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingShortcut : MonoBehaviour
{
    public Transform gaugeFill; // 게이지 스프라이트 (scale 조절)
    public float holdTime = 1.0f; // Space를 눌러야 하는 시간
    private float currentTime = 0f;
    private bool isHolding = false;

    public SpriteRenderer whiteOutSprite; // 화이트 아웃 효과를 줄 스프라이트
    public float fadeSpeed = 1.5f; // 페이드 속도
    private float alpha = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // Spacebar 누를 때
        {
            if (!isHolding)
            {
                isHolding = true;
                currentTime = 0f;
            }

            currentTime += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(currentTime / holdTime);
            gaugeFill.localScale = new Vector3(fillAmount * 12, 1f, 1f); // X축으로 늘림

            if (currentTime >= holdTime)
            {
                StartCoroutine(WhiteOutEffect());
            }
        }
        else
        {
            isHolding = false;
            currentTime = 0f;
            gaugeFill.localScale = new Vector3(0f, 1f, 1f); // 게이지 초기화
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
        SceneManager.LoadScene("EndingIntro"); // 엔딩 씬으로 이동
    }
}

