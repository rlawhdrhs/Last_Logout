using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectCheck : MonoBehaviour
{
    public SelectObject[] selectObjects;
    public Puzzle5Manager manager;
    public int[] answerpos;
    private bool isOnObject = false;
    private bool isCorrect = false;
    public Player player;
    public SpriteRenderer[] selectDark = new SpriteRenderer[9];
    public GameObject person;
    public GameObject O;
    public GameObject X;
    public GameObject storyHint;
    public GameObject cont;
    public SpriteRenderer whiteOutSprite;
    public bool next = false;
    public float multipleScale = 1.5f;

    private bool flowanim = false;
    public int cnt;
    public TMP_Text collectCnt;

    public PlaySound sound;
    void Start()
    {
        manager = FindObjectOfType<Puzzle5Manager>();
    }

    private void Update()
    {
        if (!flowanim)
        {
            if (isOnObject && Input.GetKeyDown(KeyCode.Space) && !next)
            {
                check_Quiz();
            }
            else if (isOnObject && Input.GetKeyDown(KeyCode.Space) && next)
            {
                if (cnt == 4)
                    manager.currentQuiz++;
                if (manager != null)
                    manager.QuizClear();
            }
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
        sound.Play();
        flowanim = true;
        player.movable = false;
        //정답 o 출력
        O.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        O.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        O.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        O.SetActive(false);
        yield return new WaitForSeconds(0.5f);


        SpriteRenderer[] sprite = new SpriteRenderer[9];
        for (int i = 0; i < 9; ++i)
        {
            sprite[i] = selectObjects[i].gameObject.GetComponent<SpriteRenderer>();
        }
        //사진 페이드 아웃
        float fadeSpeed = 1.5f; // 페이드 속도
        float alpha = 0f;
        person.SetActive(true);
        while (alpha < 1.5f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            whiteOutSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        Vector3 startPos = person.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            person.transform.position = Vector3.Lerp(startPos, new Vector3(0,0,0), elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        person.transform.position = new Vector3(0, 0, 0); // 정확한 위치 보정

        //크기 키우기
        Vector3 startScale = person.transform.localScale;
        Vector3 targetScale = startScale * multipleScale;
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            person.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        collectCnt.text = "수집한 정보 " + cnt + "/4";
        storyHint.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        cont.SetActive(true);
        player.movable = true;

        next = true;
        flowanim = false;
    }

    IEnumerator Wrong_anim()
    {
        X.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        X.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        X.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        X.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
