using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene"); // 1������������ �ٽ� ����
    }
}
