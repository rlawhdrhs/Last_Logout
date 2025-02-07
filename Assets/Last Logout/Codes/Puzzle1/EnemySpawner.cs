using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // �� ������
    public float spawnInterval = 3f; // �� ���� ����
    public Transform[] spawnPoints; // �� ���� ��ġ �迭
    private int cnt = 0;
    public int MaxSpawn = 5;

    private bool canSpawn = true; // �� ���� ���� ����

    void Start()
    {
        // �� ���� ��Ȱ��ȭ ���·� ����
        canSpawn = true;
    }

    void Update()
    {
        // �� ������ Ȱ��ȭ�Ǿ��� ���� ����
        if (canSpawn && !IsInvoking(nameof(SpawnEnemy)))
        {
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }
    }

    void SpawnEnemy()
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

    public void ActivateSpawner()
    {
        // �� ���� Ȱ��ȭ
        canSpawn = true;
    }
}
