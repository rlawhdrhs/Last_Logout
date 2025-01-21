using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ��
using UnityEngine.UI; // UI ��Ʈ�ѿ�

public class MenuController : MonoBehaviour
{
    public Transform cursor; // ���̾� ������ (Ŀ��)
    public Transform[] menuOptions; // �޴� �ɼǵ� (�α���, �ɼ�, �α׾ƿ�)
    private int currentIndex = 0; // ���� ���õ� �޴�

    void Start()
    {
        UpdateCursorPosition(); // ó�� ��ġ ����
    }

    void Update()
    {
        // A Ű�� ������ �� ���� �޴��� �̵�
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
            UpdateCursorPosition();
        }

        // D Ű�� ������ �� ������ �޴��� �̵�
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentIndex = Mathf.Min(menuOptions.Length - 1, currentIndex + 1);
            UpdateCursorPosition();
        }

        // Space Ű�� ������ �� ���� ���õ� �޴� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectMenu();
        }
    }

    void UpdateCursorPosition()
    {
        // ���̾� �������� ���� ���õ� �޴� ��ġ�� �̵�
        cursor.position = menuOptions[currentIndex].position;
    }

    void SelectMenu()
    {
        switch (currentIndex)
        {
            case 0: // �α���
                Debug.Log("���� ����!");
                SceneManager.LoadScene("Intro0"); // ���� ������ �̵�
                break;
            case 1: // �ɼ�
                Debug.Log("�ɼ� �޴� ����!");
                // �ɼ� UI Ȱ��ȭ (�߰��� �ɼ� UI�� ������ ��)
                break;
            case 2: // �α׾ƿ�
                Debug.Log("���� ����!");
                Application.Quit(); // ���� ����
                break;
        }
    }
}
