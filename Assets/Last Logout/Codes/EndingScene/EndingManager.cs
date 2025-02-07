using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public int endingNumber = 1;        //1 : 배드엔딩, 2 : 노말엔딩. 3 : 굿 엔딩, 4 : 진 엔딩

    void Start()
    {
        if(GameManager.instance == null)
        {
            endingNumber = 1;
        }
        else  //메인 의뢰 클리어, 0,1로 설정
        {
            bool[] PuzzleClear = new bool[7];
            PuzzleClear = GameManager.instance.PuzzleClear;
            if (MainClear(PuzzleClear))
            {
                endingNumber++;
                if (HiddenClear(PuzzleClear))
                {
                    endingNumber++;
                    if (SubClear(PuzzleClear))
                    {
                        endingNumber++;
                    }
                }
            }
        }
    }
    public void LoadEnding(int endingNumber)
    {
        SceneManager.LoadScene("Ending#" + endingNumber);
    }

    public bool MainClear(bool[] PuzzleClear)
    {
        if ((PuzzleClear[0] || PuzzleClear[2]) && (PuzzleClear[3] || PuzzleClear[1]))       //메인 의뢰 Puzzle 클리어 여부, 퍼즐 번호 순으로 정함
            return true;
        return false;
    }

    public bool HiddenClear(bool[] PuzzleClear)
    {
        if (PuzzleClear[4])             //히든 의뢰
            return true;
        return false;
    }
    public bool SubClear(bool[] PuzzleClear)
    {
        if (PuzzleClear[5] && PuzzleClear[6])       //서브 의뢰
            return true;
        return false;
    }
}
