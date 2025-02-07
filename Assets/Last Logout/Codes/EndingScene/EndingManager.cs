using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public int endingNumber = 1;        //1 : ��忣��, 2 : �븻����. 3 : �� ����, 4 : �� ����

    void Start()
    {
        if(GameManager.instance == null)
        {
            endingNumber = 1;
        }
        else  //���� �Ƿ� Ŭ����, 0,1�� ����
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
        if ((PuzzleClear[0] || PuzzleClear[2]) && (PuzzleClear[3] || PuzzleClear[1]))       //���� �Ƿ� Puzzle Ŭ���� ����, ���� ��ȣ ������ ����
            return true;
        return false;
    }

    public bool HiddenClear(bool[] PuzzleClear)
    {
        if (PuzzleClear[4])             //���� �Ƿ�
            return true;
        return false;
    }
    public bool SubClear(bool[] PuzzleClear)
    {
        if (PuzzleClear[5] && PuzzleClear[6])       //���� �Ƿ�
            return true;
        return false;
    }
}
