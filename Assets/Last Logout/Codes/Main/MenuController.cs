using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환용
using UnityEngine.UI; // UI 컨트롤용

public class MenuController : MonoBehaviour
{
    public Transform cursor; // 다이아 아이콘 (커서)
    public Transform[] menuOptions; // 메뉴 옵션들 (로그인, 옵션, 로그아웃)
    private int currentIndex = 0; // 현재 선택된 메뉴

    void Start()
    {
        UpdateCursorPosition(); // 처음 위치 설정
    }

    void Update()
    {
        // A 키를 눌렀을 때 왼쪽 메뉴로 이동
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
            UpdateCursorPosition();
        }

        // D 키를 눌렀을 때 오른쪽 메뉴로 이동
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentIndex = Mathf.Min(menuOptions.Length - 1, currentIndex + 1);
            UpdateCursorPosition();
        }

        // Space 키를 눌렀을 때 현재 선택된 메뉴 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectMenu();
        }
    }

    void UpdateCursorPosition()
    {
        // 다이아 아이콘을 현재 선택된 메뉴 위치로 이동
        cursor.position = menuOptions[currentIndex].position;
    }

    void SelectMenu()
    {
        switch (currentIndex)
        {
            case 0: // 로그인
                Debug.Log("게임 시작!");
                SceneManager.LoadScene("Intro0"); // 게임 씬으로 이동
                break;
            case 1: // 옵션
                Debug.Log("옵션 메뉴 열기!");
                // 옵션 UI 활성화 (추가로 옵션 UI를 만들어야 함)
                break;
            case 2: // 로그아웃
                Debug.Log("게임 종료!");
                Application.Quit(); // 게임 종료
                break;
        }
    }
}
