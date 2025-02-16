using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public SpriteRenderer playerSprite;

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
        StartCoroutine(InvincibilityEffect());
        if (life <= 0)
        {
            GameOver();
            return;
        }
    }
    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); // Ŭ���� ������ �̵�
    }

    public void GameClear()
    {
        if (GameManager.instance != null)
            GameManager.instance.SetPuzzleCleared(3); // Ŭ���� ���� ����
        SceneManager.LoadScene("GameClearScene"); // Ŭ���� ������ �̵�
    }

    IEnumerator InvincibilityEffect()
    {
        playerSprite.color = Color.red; // �浹 �� ������ ����

        // �����̱� ȿ��
        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.color = Color.white; // ���� ���� ����
    }
}
