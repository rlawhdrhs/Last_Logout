using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ��
using UnityEngine.UI; // UI ��Ʈ�ѿ�

public class MenuController : MonoBehaviour
{
    public Transform cursor; // ���̾� ������ (Ŀ��)
    public Transform[] menuOptions; // �޴� �ɼǵ� (���� ����, �ɼ�, �α׾ƿ�)
    public SpriteRenderer[] menuSprites; // �� �޴��� SpriteRenderer
    public Sprite frame1; // �⺻ ������ ��������Ʈ
    public Sprite frame2; // ���õǾ��� ���� ��������Ʈ
    private int currentIndex = 0; // ���� ���õ� �޴�

    public GameObject optionWindow; // �ɼ� â
    private bool option_open = false; // �ɼ� â Ȱ��ȭ ����

    public int optionIndex = 0; // �ɼ� â �� ���õ� �׸� (0: �������, 1: ȿ����)
    private int bgmVolume = 5; // ������� ���� (0~10)
    private int sfxVolume = 5; // ȿ���� ���� (0~10)

    public GameObject[] bgmBars; // ������� ������ ��Ÿ���� �׸� ��������Ʈ �迭
    public GameObject[] sfxBars; // ȿ���� ������ ��Ÿ���� �׸� ��������Ʈ �迭
    public AudioSource bgmSource; // ������� AudioSource
    public AudioSource sfxSource; // ȿ���� AudioSource
    public AudioClip selectSound; // Spacebar ���� �� ����� ȿ����

    void Start()
    {
        UpdateCursorPosition();
        UpdateMenuSprites();
        optionWindow.SetActive(false);
        close_VolumeUI();
        ApplyVolume(); // ���� ���� �� ���� ����
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

                ApplyVolume(); // ���� ����
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (optionIndex == 1)
                    bgmVolume = Mathf.Min(10, bgmVolume + 1);
                else if (optionIndex == 2)
                    sfxVolume = Mathf.Min(10, sfxVolume + 1);

                ApplyVolume(); // ���� ����
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
            // �޴� �̵�
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

            // Space Ű: �޴� ����
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
            case 0: // ���� ����
                Debug.Log("���� ����!");
                SceneManager.LoadScene("Intro0");
                break;
            case 1: // �ɼ�
                Debug.Log("�ɼ� �޴� ����!");
                optionWindow.SetActive(true);
                option_open = true;
                optionIndex = 0;
                UpdateCursorPosition();
                break;
            case 2: // �α׾ƿ�
                Debug.Log("���� ����!");
                Application.Quit();
                break;
        }
    }

    void UpdateVolumeUI()
    {
        // ������� ���� UI ������Ʈ
        for (int i = 0; i < bgmBars.Length; i++)
        {
            bgmBars[i].SetActive(i < bgmVolume);
        }

        // ȿ���� ���� UI ������Ʈ
        for (int i = 0; i < sfxBars.Length; i++)
        {
            sfxBars[i].SetActive(i < sfxVolume);
        }
    }

    void close_VolumeUI()
    {
        // ������� ���� UI ������Ʈ
        for (int i = 0; i < bgmBars.Length; i++)
        {
            bgmBars[i].SetActive(false);
        }

        // ȿ���� ���� UI ������Ʈ
        for (int i = 0; i < sfxBars.Length; i++)
        {
            sfxBars[i].SetActive(false);
        }
    }

    void ApplyVolume()
    {
        bgmSource.volume = bgmVolume / 10.0f; // 0~10�� 0~1 ������ ��ȯ
        sfxSource.volume = sfxVolume / 50.0f;
    }
}
