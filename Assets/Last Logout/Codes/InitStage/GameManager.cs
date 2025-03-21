using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool[]PuzzleClear = new bool[7];
    public bool[]PuzzleFail = new bool[7];
    public bool moveToPortal = false;
    public string beforeMap;
    public int currentPuzzle = 0;
    public bool cur;
    public bool isOpenMission = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
        init_();
        beforeMap = "GameScene";
    }
    void init_()
    {
        for(int i = 0; i < 7; ++i)
        {
            PuzzleClear[i] = false;
            PuzzleFail[i] = false;
        }
    }
    public void SetPuzzleCleared(int puzzleIndex)
    {
        if (puzzleIndex >= 0 && puzzleIndex < PuzzleClear.Length)
        {
            PuzzleClear[puzzleIndex] = true;
        }
    }
    public bool IsPuzzleCleared(int puzzleIndex)
    {
        return (puzzleIndex >= 0 && puzzleIndex < PuzzleClear.Length) && PuzzleClear[puzzleIndex];
    }
}
