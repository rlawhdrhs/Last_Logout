using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle4Player : MonoBehaviour
{
    public Text inputText; // 플레이어 입력을 표시할 UI
    private List<string> playerInputs = new List<string>(); // 입력한 키 저장

    public Puzzle4WallManager manager;
    public int ClearPoint = 10;
    public int PointCount = 0;
    public PlaySound delete;
    public PlaySound input;
    public TMP_Text Ui;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            string pressedKey = GetPressedKey();
            if (!string.IsNullOrEmpty(pressedKey))
            {
                Puzzle4Wall closestWall = FindClosestWall();
                if (closestWall != null)
                {
                    string requiredKeys = closestWall.GetRequiredKeys();
                    string[] keyArray = requiredKeys.Split(' ');

                    // 입력한 키가 문제에 포함되어 있으면 추가
                    if (playerInputs.Count < keyArray.Length && pressedKey == keyArray[playerInputs.Count])
                    {
                        input.Play();
                        playerInputs.Add(pressedKey);
                        UpdateDisplay();
                    }

                    // 모든 키를 입력하면 벽 제거
                    if (playerInputs.Count == keyArray.Length)
                    {
                        delete.Play();
                        Destroy(closestWall.gameObject);
                        playerInputs.Clear();
                        PointCount++;
                        inputText.text = "";
                    }
                }
            }
        }
        UpdateUi();

        if (PointCount == ClearPoint)
        {
            manager.GameClear();
        }
    }

    string GetPressedKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) return "←";
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) return "→";
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) return "↑";
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) return "↓";
        return "";
    }

    void UpdateDisplay()
    {
        inputText.text = string.Join(" ", playerInputs);
    }

    Puzzle4Wall FindClosestWall()
    {
        Puzzle4Wall[] walls = FindObjectsOfType<Puzzle4Wall>();
        if (walls.Length == 0) return null;

        Puzzle4Wall closest = walls[0];
        float minDist = Vector3.Distance(transform.position, closest.transform.position);

        foreach (Puzzle4Wall wall in walls)
        {
            float dist = Vector3.Distance(transform.position, wall.transform.position);
            if (dist < minDist)
            {
                closest = wall;
                minDist = dist;
            }
        }
        return closest;
    }

    void UpdateUi()
    {
        Ui.text = PointCount + "/" + ClearPoint;
    }

    public void ClearInput()
    {
        playerInputs.Clear(); // 입력 리스트 초기화
        inputText.text = ""; // UI 텍스트 초기화
    }
}
