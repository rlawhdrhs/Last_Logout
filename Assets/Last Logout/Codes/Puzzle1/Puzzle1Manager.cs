using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle1Manager : MonoBehaviour
{
    public static Puzzle1Manager instance;
    public int enemyCount; // ���� ��� �ִ� �� ����
    public EnemySpawner spawner;
    public bool drop = false;
    public TMP_Text EnemyCnt;
    void Awake()
    {
        if (instance == null)
            instance = this;
        enemyCount = spawner.MaxSpawn;
        StartCoroutine(SpawnLevel());
    }
    private void Update()
    {
        EnemyCnt.text = "���� ���̷��� : " + enemyCount;
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
    IEnumerator SpawnLevel()
    {
        while (spawner.canSpawn)
        {
            float currentTime = Time.timeSinceLevelLoad;
            if (currentTime < 30f) // 0~30��
            {
                yield return StartCoroutine(spawner.SpawnWave(10, 3f));
            }
            else if (currentTime < 60f) // 30~60��
            {
                yield return StartCoroutine(spawner.SpawnWave(15, 2f));
            }
            else if (currentTime < 70f) // 60~70��
            {
                yield return StartCoroutine(spawner.SpawnWave(10, 1f));
            }
            else if (currentTime < 75f) // 70~75�� (���� �ð�)
            {
                yield return new WaitForSeconds(5f);
            }
            else if (currentTime < 84f) // 75~84��
            {
                yield return StartCoroutine(spawner.SpawnWave(3, 3f));
            }
            else if (currentTime < 96f) // 84~96��
            {
                yield return StartCoroutine(spawner.SpawnWave(6, 2f));
            }
            else if (currentTime < 110f) // 96~110��
            {
                for(int i = 0; i < 4; ++i)
                    spawner.SpawnEnemy();
                yield return new WaitForSeconds(14f);
            }
            else if (currentTime < 125f) // 110~125��
            {
                for (int i = 0; i < 7; ++i)
                    spawner.SpawnEnemy();
                yield return new WaitForSeconds(15f);
            }
            else if(currentTime < 130) //125~130��
            {
                for (int i = 0; i < 5; ++i)
                    spawner.SpawnEnemy();
                yield return new WaitForSeconds(5f);
            }
            else if (currentTime < 300) //130~������ ���̷��� �������
            {
                yield return StartCoroutine(spawner.SpawnWave(15, 1f));
            }
            else
            {
                yield break; // ����
            }
        }
    }
}
