using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle1Manager : MonoBehaviour
{
    public static Puzzle1Manager instance;
    public int enemyCount; // ���� ��� �ִ� �� ����
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
            GameManager.instance.SetPuzzleCleared(0); // Ŭ���� ���� ����
        SceneManager.LoadScene("GameClearScene"); // Ŭ���� ������ �̵�
    }
}
