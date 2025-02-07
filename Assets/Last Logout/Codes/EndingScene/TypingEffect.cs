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
        dialogue = "메일이 도착했습니다.\n\n확인하려면 메일을 클릭하세요.";
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
