using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMove : MonoBehaviour
{
    public float introDuration = 3f; // ��Ʈ�� ���� �ð� (��)
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
        SceneManager.LoadScene("GameScene");
    }
}
