using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // �� ������
    public float spawnInterval = 3f; // �� ���� ����
    public Transform[] spawnPoints; // �� ���� ��ġ �迭
    private int cnt = 0;
    public int MaxSpawn = 75;

    public bool canSpawn = true; // �� ���� ���� ����

    void Start()
    {
        // �� ���� Ȱ��ȭ ���·� ����
        canSpawn = true;
    }

    void Update()
    {
        // �� ������ Ȱ��ȭ�Ǿ��� ���� ����
        /*
        if (canSpawn && !IsInvoking(nameof(SpawnEnemy)))
        {
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }
        */
    }

    public void SpawnEnemy()
    {
        if (!canSpawn) return;

        cnt++;
        // ������ ��ġ���� �� ����
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        if (cnt >= MaxSpawn)
        {
            canSpawn = false;
            CancelInvoke(nameof(SpawnEnemy));
        }
    }
    public IEnumerator SpawnWave(int enemyCount, float spawnInterval)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (!canSpawn) yield break;

            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void ActivateSpawner()
    {
        // �� ���� Ȱ��ȭ
        canSpawn = true;
    }
}
