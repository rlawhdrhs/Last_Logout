using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Puzzle7Manager : MonoBehaviour
{
    public int collectPI = 0;  //���� ���� ���� ����
    public int maxcollectPI = 10;
    public TMP_Text collectUi;
    public TMP_Text[] PersonalId = new TMP_Text[10];
    public TMP_Text clearText;
    public SpriteRenderer whiteOutSprite; // ȭ��Ʈ �ƿ� ȿ���� �� ��������Ʈ
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
        clearText.text = "������ �����մϴ�.";
        player.TriggerFall();
        yield return new WaitForSeconds(1f);

        float fadeSpeed = 1.5f; // ���̵� �ӵ�
        float alpha = 0f;
        while (alpha < 1.5f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            whiteOutSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        SceneManager.LoadScene("GameOverScene"); // ���� ���� ������ �̵�
    }
    void GameClear()
    {
        clearText.text = "Ŭ����!";
        StartCoroutine(bomb.BombAnim());
    }
}
