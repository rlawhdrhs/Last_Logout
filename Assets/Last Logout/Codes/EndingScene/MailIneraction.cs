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
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ����
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject) // Ŭ���� ������Ʈ�� �ڽ����� Ȯ��
            {
                mailOpenObject.SetActive(true); // ���� ���� Ȱ��ȭ
                StartCoroutine(WhiteOutEffect()); // ȭ��Ʈ �ƿ� ȿ�� ����
            }
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

