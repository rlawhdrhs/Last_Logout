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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ���콺 ���� ��ư Ŭ�� ����
        {
            mailOpenObject.SetActive(true); // ���� ���� Ȱ��ȭ
            StartCoroutine(WhiteOutEffect()); // ȭ��Ʈ �ƿ� ȿ�� ����
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

