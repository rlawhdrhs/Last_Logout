using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool[]PuzzleClear = new bool[7];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ¾ÀÀÌ ¹Ù²î¾îµµ À¯ÁöµÊ
        }
        else
        {
            Destroy(gameObject); // Áßº¹ ¹æÁö
        }
        init_();
    }
    void init_()
    {
        for(int i = 0; i < 7; ++i)
        {
            PuzzleClear[i] = false;
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
