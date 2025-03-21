using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailInteraction : MonoBehaviour
{
    public GameObject mailOpenObject; // Ŭ�� �� Ȱ��ȭ�� ������Ʈ (���� ����)
    public SpriteRenderer whiteOutSprite; // ȭ��Ʈ �ƿ� ȿ���� �� ��������Ʈ
    public float fadeSpeed = 1.5f; // ���̵� �ӵ�
    public EndingManager endingManager;
    private float alpha = 0f;
    private bool isInputBlocked = true; // �Է� ���� ����
    public PlaySound sound;
    void Start()
    {
        StartCoroutine(StopSec()); // ���� ���۵Ǹ� 2�� �Ŀ� �Է� Ȱ��ȭ
    }

    void Update()
    {
        if (isInputBlocked) return; // �Է� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sound.StopSound();
            mailOpenObject.SetActive(true); // ���� ���� Ȱ��ȭ
            StartCoroutine(WhiteOutEffect()); // ȭ��Ʈ �ƿ� ȿ�� ����
        }
    }
    IEnumerator StopSec()
    {
        yield return new WaitForSeconds(2f);
        isInputBlocked = false; // 2�� �� �Է� ���
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

