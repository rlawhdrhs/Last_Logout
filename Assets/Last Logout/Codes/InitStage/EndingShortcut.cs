using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingShortcut : MonoBehaviour
{
    public Transform gaugeFill; // ������ ��������Ʈ (scale ����)
    public float holdTime = 1.0f; // Space�� ������ �ϴ� �ð�
    private float currentTime = 0f;
    private bool isHolding = false;

    public SpriteRenderer whiteOutSprite; // ȭ��Ʈ �ƿ� ȿ���� �� ��������Ʈ
    public float fadeSpeed = 1.5f; // ���̵� �ӵ�
    private float alpha = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // Spacebar ���� ��
        {
            if (!isHolding)
            {
                isHolding = true;
                currentTime = 0f;
            }

            currentTime += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(currentTime / holdTime);
            gaugeFill.localScale = new Vector3(fillAmount * 12, 1f, 1f); // X������ �ø�

            if (currentTime >= holdTime)
            {
                StartCoroutine(WhiteOutEffect());
            }
        }
        else
        {
            isHolding = false;
            currentTime = 0f;
            gaugeFill.localScale = new Vector3(0f, 1f, 1f); // ������ �ʱ�ȭ
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
        SceneManager.LoadScene("EndingIntro"); // ���� ������ �̵�
    }
}

