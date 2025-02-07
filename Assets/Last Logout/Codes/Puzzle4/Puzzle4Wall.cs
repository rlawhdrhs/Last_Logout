using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle4Wall : MonoBehaviour
{
    public TMP_Text textMesh;  // Text → TMP_Text로 변경
    public float speed = 2f;
    private string requiredKeys;
    public Puzzle4WallManager GameManager;
    private Puzzle4Player player;

    void Start()
    {
        GameManager = FindObjectOfType<Puzzle4WallManager>();
        player = FindObjectOfType<Puzzle4Player>();
        requiredKeys = GenerateRandomKeys();
        textMesh.text = requiredKeys;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    string GenerateRandomKeys()
    {
        string[] keys = { "←", "→", "↑", "↓" };
        string result = "";

        for (int i = 0; i < Random.Range(7, 10); i++) // 최소 7개 방향키 문제
        {
            result += keys[Random.Range(0, keys.Length)] + " ";
        }
        return result.Trim();
    }

    public string GetRequiredKeys()
    {
        return requiredKeys;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.ReduceLife(); // 체력 감소
            player.ClearInput(); // 입력한 값 초기화
            requiredKeys = ""; // 벽 문제 삭제
            textMesh.text = requiredKeys;
        }
    }
}
