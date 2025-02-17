using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Puzzle7Manager : MonoBehaviour
{
    public int collectPI = 0;  //개인 정보 수집 갯수
    public int maxcollectPI = 10;
    public TMP_Text collectUi;
    public TMP_Text[] PersonalId = new TMP_Text[10];
    public TMP_Text clearText;
    public SpriteRenderer whiteOutSprite; // 화이트 아웃 효과를 줄 스프라이트
    public Puzzle7Player player;
    public Bomb bomb;

    void Update()
    {
        UpdateCollectUi();
    }

    void UpdateCollectUi()
    {
        collectUi.text = collectPI + "/" + maxcollectPI;
    }
    public void GameEnd()
    {
        if(collectPI < maxcollectPI)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            GameClear();
        }
    }
    IEnumerator GameOver()
    {
        clearText.text = "정보가 부족합니다.";
        player.TriggerFall();
        yield return new WaitForSeconds(1f);

        float fadeSpeed = 1.5f; // 페이드 속도
        float alpha = 0f;
        while (alpha < 1.5f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            whiteOutSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        SceneManager.LoadScene("GameOverScene"); // 게임 오버 씬으로 이동
    }
    void GameClear()
    {
        clearText.text = "클리어!";
        StartCoroutine(bomb.BombAnim());
    }
}
