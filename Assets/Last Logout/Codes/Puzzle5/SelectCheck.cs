using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class SelectCheck : MonoBehaviour
{
    public SelectObject[] selectObjects;
    public Puzzle5Manager manager;
    public int[] answerpos;
    private bool isOnObject = false;
    private bool isCorrect = false;

    public GameObject O;
    public GameObject X;
    void Start()
    {
        manager = FindObjectOfType<Puzzle5Manager>();
    }

    private void Update()
    {
        if (isOnObject && Input.GetKeyDown(KeyCode.Space))
        {
            check_Quiz();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 캐릭터와 충돌했을 때
        {
            isOnObject = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 캐릭터가 오브젝트에서 벗어났을 때
        {
            isOnObject = false;
        }
    }

    void check_Quiz()
    {
        int cnt = 0;
        for(int i = 0; i < answerpos.Length; i++)
        {
            if (selectObjects[answerpos[i]].isSelected)
            {
                cnt++;
            }
        }
        if(cnt == answerpos.Length)
            isCorrect = true;

        if (isCorrect)
            StartCoroutine(Correct_anim());
        else
        {
            StartCoroutine(Wrong_anim());
            manager.ReduceLife();
        }
    }

    IEnumerator Correct_anim()
    {
        O.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        O.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        manager.QuizClear();
    }

    IEnumerator Wrong_anim()
    {
        X.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        X.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
