using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle1Manager : MonoBehaviour
{
    public static Puzzle1Manager instance;
    public int enemyCount; // 현재 살아 있는 적 개수
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
        EnemyCnt.text = "남은 바이러스 : " + enemyCount;
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
    IEnumerator SpawnLevel()
    {
        while (spawner.canSpawn)
        {
            float currentTime = Time.timeSinceLevelLoad;
            if (currentTime < 30f) // 0~30초
            {
                yield return StartCoroutine(spawner.SpawnWave(10, 3f));
            }
            else if (currentTime < 60f) // 30~60초
            {
                yield return StartCoroutine(spawner.SpawnWave(15, 2f));
            }
            else if (currentTime < 70f) // 60~70초
            {
                yield return StartCoroutine(spawner.SpawnWave(10, 1f));
            }
            else if (currentTime < 75f) // 70~75초 (쉬는 시간)
            {
                yield return new WaitForSeconds(5f);
            }
            else if (currentTime < 84f) // 75~84초
            {
                yield return StartCoroutine(spawner.SpawnWave(3, 3f));
            }
            else if (currentTime < 96f) // 84~96초
            {
                yield return StartCoroutine(spawner.SpawnWave(6, 2f));
            }
            else if (currentTime < 110f) // 96~110초
            {
                for(int i = 0; i < 4; ++i)
                    spawner.SpawnEnemy();
                yield return new WaitForSeconds(14f);
            }
            else if (currentTime < 125f) // 110~125초
            {
                for (int i = 0; i < 7; ++i)
                    spawner.SpawnEnemy();
                yield return new WaitForSeconds(15f);
            }
            else if(currentTime < 130) //125~130초
            {
                for (int i = 0; i < 5; ++i)
                    spawner.SpawnEnemy();
                yield return new WaitForSeconds(5f);
            }
            else if (currentTime < 300) //130~마지막 바이러스 사망까지
            {
                yield return StartCoroutine(spawner.SpawnWave(15, 1f));
            }
            else
            {
                yield break; // 종료
            }
        }
    }
}
