using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle1Manager : MonoBehaviour
{
    public static Puzzle1Manager instance;
    public int enemyCount; // 현재 살아 있는 적 개수
    public EnemySpawner spawner;
    public bool drop = false;
    void Awake()
    {
        if (instance == null)
            instance = this;
        enemyCount = spawner.MaxSpawn;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            GameClear();
        }
    }

    void GameClear()
    {
        if (GameManager.instance != null)
            GameManager.instance.SetPuzzleCleared(0); // 클리어 상태 저장
        SceneManager.LoadScene("GameClearScene"); // 클리어 씬으로 이동
    }
}
