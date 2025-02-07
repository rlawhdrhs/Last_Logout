using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Puzzle4WallManager : MonoBehaviour
{
    public GameObject wallPrefab;
    public Transform wallSpawnPoint;
    public float wallSpawnInterval = 3f; // �� ���� ����
    private List<GameObject> activeWalls = new List<GameObject>();
    public SpriteRenderer[] hearts; // ��Ʈ UI �迭 (3��)
    public Sprite fullHeart; // �� �� ��Ʈ ��������Ʈ
    public Sprite emptyHeart; // �� ��Ʈ ��������Ʈ
    public Puzzle4Player player;

    private int life = 3; // �ʱ� ü�� 3��

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
        if (life <= 0) return; // ü���� 0�̸� ���� X

        life--; // ü�� ����
        hearts[life].sprite = emptyHeart; // �ش� ��Ʈ�� �� ��Ʈ�� ����
        if (life <= 0)
        {
            return;
        }
    }
}
