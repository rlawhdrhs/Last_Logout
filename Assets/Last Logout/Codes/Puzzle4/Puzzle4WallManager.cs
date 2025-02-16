using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle4WallManager : MonoBehaviour
{
    public GameObject wallPrefab;
    public Transform wallSpawnPoint;
    public float wallSpawnInterval = 3f; // 벽 생성 간격
    private List<GameObject> activeWalls = new List<GameObject>();
    public SpriteRenderer[] hearts; // 하트 UI 배열 (3개)
    public Sprite fullHeart; // 꽉 찬 하트 스프라이트
    public Sprite emptyHeart; // 빈 하트 스프라이트
    public Puzzle4Player player;
    public SpriteRenderer playerSprite;

    private int life = 3; // 초기 체력 3개

    void Start()
    {
        StartCoroutine(SpawnWalls());
    }

    IEnumerator SpawnWalls()
    {
        while (true)
        {
            GameObject newWall = Instantiate(wallPrefab, wallSpawnPoint.position, Quaternion.identity);
            activeWalls.Add(newWall);
            yield return new WaitForSeconds(wallSpawnInterval);
        }
    }
    public void ReduceLife()
    {
        if (life <= 0) return; // 체력이 0이면 실행 X

        life--; // 체력 감소
        hearts[life].sprite = emptyHeart; // 해당 하트를 빈 하트로 변경
        StartCoroutine(InvincibilityEffect());
        if (life <= 0)
        {
            GameOver();
            return;
        }
    }
    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); // 클리어 씬으로 이동
    }

    public void GameClear()
    {
        if (GameManager.instance != null)
            GameManager.instance.SetPuzzleCleared(3); // 클리어 상태 저장
        SceneManager.LoadScene("GameClearScene"); // 클리어 씬으로 이동
    }

    IEnumerator InvincibilityEffect()
    {
        playerSprite.color = Color.red; // 충돌 시 빨간색 변경

        // 깜빡이기 효과
        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.color = Color.white; // 원래 색상 복구
    }
}
