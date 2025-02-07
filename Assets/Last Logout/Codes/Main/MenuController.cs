using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환용
using UnityEngine.UI; // UI 컨트롤용

public class MenuController : MonoBehaviour
{
    public Transform cursor; // 다이아 아이콘 (커서)
    public Transform[] menuOptions; // 메뉴 옵션들 (게임 시작, 옵션, 로그아웃)
    public SpriteRenderer[] menuSprites; // 각 메뉴의 SpriteRenderer
    public Sprite frame1; // 기본 상태의 스프라이트
    public Sprite frame2; // 선택되었을 때의 스프라이트
    private int currentIndex = 0; // 현재 선택된 메뉴

    public GameObject optionWindow; // 옵션 창
    private bool option_open = false; // 옵션 창 활성화 여부

    public int optionIndex = 0; // 옵션 창 내 선택된 항목 (0: 배경음악, 1: 효과음)
    private int bgmVolume = 5; // 배경음악 볼륨 (0~10)
    private int sfxVolume = 5; // 효과음 볼륨 (0~10)

    public GameObject[] bgmBars; // 배경음악 볼륨을 나타내는 네모 스프라이트 배열
    public GameObject[] sfxBars; // 효과음 볼륨을 나타내는 네모 스프라이트 배열
    public AudioSource bgmSource; // 배경음악 AudioSource
    public AudioSource sfxSource; // 효과음 AudioSource
    public AudioClip selectSound; // Spacebar 누를 때 재생할 효과음

    void Start()
    {
        UpdateCursorPosition();
        UpdateMenuSprites();
        optionWindow.SetActive(false);
        close_VolumeUI();
        ApplyVolume(); // 게임 시작 시 볼륨 적용
    }

    void Update()
    {
        bool moved = false;

        if (option_open)
        {
            UpdateVolumeUI();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                optionIndex = Mathf.Max(0, optionIndex - 1);
                moved = true;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                optionIndex = Mathf.Min(2, optionIndex + 1);
                moved = true;
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (optionIndex == 1)
                    bgmVolume = Mathf.Max(0, bgmVolume - 1);
                else if (optionIndex == 2)
                    sfxVolume = Mathf.Max(0, sfxVolume - 1);

                ApplyVolume(); // 볼륨 적용
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (optionIndex == 1)
                    bgmVolume = Mathf.Min(10, bgmVolume + 1);
                else if (optionIndex == 2)
                    sfxVolume = Mathf.Min(10, sfxVolume + 1);

                ApplyVolume(); // 볼륨 적용
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionWindow.SetActive(false);
                option_open = false;
                currentIndex = 1;
                UpdateCursorPosition();
                UpdateMenuSprites();
                close_VolumeUI();
            }
            if (moved)
            {
                if (sfxSource != null && selectSound != null)
                {
                    sfxSource.PlayOneShot(selectSound);
                }
                UpdateCursorPosition();
                UpdateMenuSprites();
            }
        }
        else
        {
            // 메뉴 이동
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentIndex = Mathf.Max(0, currentIndex - 1);
                moved = true;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentIndex = Mathf.Min(2, currentIndex + 1);
                moved = true;
            }

            if (moved)
            {
                if (sfxSource != null && selectSound != null)
                {
                    sfxSource.PlayOneShot(selectSound);
                }
                UpdateCursorPosition();
                UpdateMenuSprites();
            }

            // Space 키: 메뉴 선택
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (sfxSource != null && selectSound != null)
                {
                    sfxSource.PlayOneShot(selectSound);
                }
                SelectMenu();
            }
        }
    }

    void UpdateCursorPosition()
    {
        if (option_open)
        {
            cursor.position = menuOptions[optionIndex + 3].position;
        }
        else
        {
            cursor.position = menuOptions[currentIndex].position;
        }
    }

    void UpdateMenuSprites()
    {
        for (int i = 0; i < menuSprites.Length; i++)
        {
            menuSprites[i].sprite = (i == currentIndex) ? frame2 : frame1;
        }
    }

    void SelectMenu()
    {
        switch (currentIndex)
        {
            case 0: // 게임 시작
                Debug.Log("게임 시작!");
                SceneManager.LoadScene("Intro0");
                break;
            case 1: // 옵션
                Debug.Log("옵션 메뉴 열기!");
                optionWindow.SetActive(true);
                option_open = true;
                optionIndex = 0;
                UpdateCursorPosition();
                break;
            case 2: // 로그아웃
                Debug.Log("게임 종료!");
                Application.Quit();
                break;
        }
    }

    void UpdateVolumeUI()
    {
        // 배경음악 볼륨 UI 업데이트
        for (int i = 0; i < bgmBars.Length; i++)
        {
            bgmBars[i].SetActive(i < bgmVolume);
        }

        // 효과음 볼륨 UI 업데이트
        for (int i = 0; i < sfxBars.Length; i++)
        {
            sfxBars[i].SetActive(i < sfxVolume);
        }
    }

    void close_VolumeUI()
    {
        // 배경음악 볼륨 UI 업데이트
        for (int i = 0; i < bgmBars.Length; i++)
        {
            bgmBars[i].SetActive(false);
        }

        // 효과음 볼륨 UI 업데이트
        for (int i = 0; i < sfxBars.Length; i++)
        {
            sfxBars[i].SetActive(false);
        }
    }

    void ApplyVolume()
    {
        bgmSource.volume = bgmVolume / 10.0f; // 0~10을 0~1 범위로 변환
        sfxSource.volume = sfxVolume / 50.0f;
    }
}
