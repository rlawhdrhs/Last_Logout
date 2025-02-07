using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public float introDuration = 3f; // 인트로 유지 시간 (초)
    public int currentScene = 0;
    public int maxScene = 5;

    void Start()
    {
        // 일정 시간이 지나면 자동으로 게임 씬으로 이동
        Invoke("LoadNextScene", introDuration);
    }

    void Update()
    {
        // 사용자가 Space 키 또는 마우스를 클릭하면 즉시 게임 씬으로 이동
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        currentScene++;
        if(currentScene == maxScene)
        {
            SceneManager.LoadScene("GameScene");
            return;
        }
        SceneManager.LoadScene("Intro" + currentScene); // 게임 씬 로드
    }
}
