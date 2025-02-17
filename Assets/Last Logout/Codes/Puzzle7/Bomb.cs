using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public Puzzle7Manager manager;
    public GameObject[] fire = new GameObject[4];
    public GameObject bomb_fire;
    public GameObject String;
    public SpriteRenderer whiteOutSprite; // 화이트 아웃 효과를 줄 스프라이트
    public Puzzle7Player player;
    private void Start()
    {
        for(int i = 0;i<4;++i)
            fire[i].SetActive(false);
        bomb_fire.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            manager.GameEnd();
            player.falling = true;
        }
    }

    public IEnumerator BombAnim()
    {
        for (int i = 0; i < 4; ++i)
        {
            fire[i].SetActive(true);


            for (int j = 0; j <= i; ++j)
            {
                SpriteRenderer sprite = fire[j].GetComponent<SpriteRenderer>();
                Color originalColor = sprite.color;
                sprite.color = Color.red;
                yield return new WaitForSeconds(0.1f); //색이 바뀌는 타이밍을 더 명확하게
                sprite.color = originalColor;
            }

            yield return new WaitForSeconds(0.5f); //불이 하나씩 붙는 연출 추가
        }
        String.SetActive(false);
        gameObject.SetActive(false);
        bomb_fire.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        float fadeSpeed = 1.5f; // 페이드 속도
        float alpha = 0f;
        while (alpha < 1.5f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            whiteOutSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        if (GameManager.instance != null)
        {
            GameManager.instance.PuzzleClear[6] = true;
        }
        SceneManager.LoadScene("GameClearScene"); // 게임 클리어 씬으로 이동
    }
}
