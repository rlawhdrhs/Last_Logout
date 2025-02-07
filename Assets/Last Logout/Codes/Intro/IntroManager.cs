using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public float introDuration = 3f; // ��Ʈ�� ���� �ð� (��)
    public int currentScene = 0;
    public int maxScene = 5;

    void Start()
    {
        // ���� �ð��� ������ �ڵ����� ���� ������ �̵�
        Invoke("LoadNextScene", introDuration);
    }

    void Update()
    {
        // ����ڰ� Space Ű �Ǵ� ���콺�� Ŭ���ϸ� ��� ���� ������ �̵�
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
        SceneManager.LoadScene("Intro" + currentScene); // ���� �� �ε�
    }
}
