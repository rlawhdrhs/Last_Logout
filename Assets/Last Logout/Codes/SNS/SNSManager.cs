using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SNSManager : MonoBehaviour
{
    public GameObject LoadingWindow;
    public GameObject ErrorWindow;
    public TMP_Text text;
    public PlaySound sound;


    public void Loading()
    {
        StartCoroutine(LoadingAnim());
    }
    public void Puzzle3()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.beforeMap = "SNS";
            GameManager.instance.currentPuzzle = 3;
        }
        SceneManager.LoadScene("Puzzle3 Stage1");
    }
    IEnumerator LoadingAnim()
    {
        LoadingWindow.SetActive(true);
        text.text = "게시글을 삭제 중";
        for (int i = 0; i < 3; ++i)
        {
            yield return new WaitForSeconds(2f);
            text.text += ".";
        }
        yield return new WaitForSeconds(2f);
        text.text = "";
        sound.Play();
        for (int i = 0; i < 6; ++i)
        {
            ErrorWindow.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            ErrorWindow.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        if (GameManager.instance != null)
        {
            GameManager.instance.beforeMap = "SNS";
            GameManager.instance.currentPuzzle = 1;
        }
        SceneManager.LoadScene("Puzzle1 Stage1");
    }
}
