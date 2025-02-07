using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text text;
    string dialogue;

    void Start()
    {
        dialogue = "������ �����߽��ϴ�.\n\nȮ���Ϸ��� ������ Ŭ���ϼ���.";
        StartCoroutine(Typing(dialogue));
    }

    IEnumerator Typing(string talk)
    {
        text.text = null;

        for(int i = 0;i<talk.Length; i++)
        {
            text.text += talk[i];

            yield return new WaitForSeconds(0.1f);
        }
    }
}
