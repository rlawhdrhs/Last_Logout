using TMPro;
using UnityEngine;

public class PuzzleClear : MonoBehaviour
{
    public SpriteRenderer[] puzzle = new SpriteRenderer[5];
    public Sprite clearedSprite; // 퍼즐 클리어 후 변경할 스프라이트
    public TMP_Text[] missionList = new TMP_Text[5];
    private int QuestNum;
    public GameObject hiddenQuest_hide;
    public GameObject hiddenQuest_open;



    private void Start()
    {
        for (int i = 0; i <= 6; i++)
        {
            if (GameManager.instance.IsPuzzleCleared(i))
            {
                if (i == 0 || i == 2)   //메인 1
                    QuestNum = 0;
                else if (i == 3 || i == 1)  //메인 2
                    QuestNum = 1;
                else if (i == 6)            //서브 2
                    QuestNum = 2;
                else if (i == 4)            //히든
                    QuestNum = 3;
                puzzle[QuestNum].sprite = clearedSprite;
                if (QuestNum < 3)
                    missionList[QuestNum].fontStyle = FontStyles.Strikethrough;
                else
                {
                    hiddenQuest_hide.SetActive(false);
                    hiddenQuest_open.SetActive(true);
                }
            }
        }
    }
}