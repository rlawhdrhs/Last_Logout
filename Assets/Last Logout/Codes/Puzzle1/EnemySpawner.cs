using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // 적 프리팹
    public float spawnInterval = 3f; // 적 생성 간격
    public Transform[] spawnPoints; // 적 생성 위치 배열
    private int cnt = 0;
    public int MaxSpawn = 5;

    private bool canSpawn = true; // 적 생성 여부 제어

    void Start()
    {
        // 적 생성 비활성화 상태로 시작
        canSpawn = true;
    }

    void Update()
    {
        // 적 생성이 활성화되었을 때만 실행
        if (canSpawn && !IsInvoking(nameof(SpawnEnemy)))
        {
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (!canSpawn) return;

        cnt++;
        // 랜덤한 위치에서 적 생성
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        if (cnt >= MaxSpawn)
        {
            canSpawn = false;
            CancelInvoke(nameof(SpawnEnemy));
        }
    }

    public void ActivateSpawner()
    {
        // 적 생성 활성화
        canSpawn = true;
    }
}
