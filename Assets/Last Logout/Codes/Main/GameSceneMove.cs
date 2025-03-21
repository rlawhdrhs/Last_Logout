using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMove : MonoBehaviour
{
    public float introDuration = 3f; // 인트로 유지 시간 (초)
    void Start()
    {
        // 일정 시간이 지나면 자동으로 게임 씬으로 이동
        Invoke("LoadNextScene", introDuration);
    }

    public void LoadNextScene()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.PuzzleFail[0] && GameManager.instance.PuzzleFail[2])
                SceneManager.LoadScene("EndingIntro");
            else if ((GameManager.instance.PuzzleClear[0] || GameManager.instance.PuzzleClear[2]) && (GameManager.instance.PuzzleClear[1] || GameManager.instance.PuzzleClear[3]) && GameManager.instance.PuzzleClear[6])
                SceneManager.LoadScene("EndingIntro");
            else if(GameManager.instance.PuzzleFail[1] && GameManager.instance.PuzzleFail[3])
                SceneManager.LoadScene("EndingIntro");
            else if(GameManager.instance.beforeMap == "SNS")
            {
                if (GameManager.instance.PuzzleClear[0] == true || GameManager.instance.PuzzleClear[2] == true)
                    SceneManager.LoadScene(GameManager.instance.beforeMap + "Clear");
                else if(GameManager.instance.PuzzleFail[0] == true)
                    SceneManager.LoadScene(GameManager.instance.beforeMap + " Stage1");
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
