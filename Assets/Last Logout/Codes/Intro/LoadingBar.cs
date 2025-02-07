using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    public GameObject[] loadingStages; // �ε� �ܰ躰 ������Ʈ
    public float delayBetweenSteps = 0.5f; // 0.5�ʸ��� Ȱ��ȭ
    public IntroManager introManager;
    public GameObject player;
    private int currentStep = 0;
    public GameObject screen_1, screen_2;

    private void Start()
    {
        // ó�� ������ �� ��� �ε� ��������Ʈ�� ����
        StartCoroutine(ScreenChange());
        HideAllLoadingStages();
        StartLoading();
    }

    private void HideAllLoadingStages()
    {
        foreach (GameObject stage in loadingStages)
        {
            stage.SetActive(false); // ��� �ε� �� �����
        }
    }

    public void StartLoading()
    {
        StartCoroutine(ShowLoadingAnimation());
    }

    IEnumerator ShowLoadingAnimation()
    {
        while (currentStep < loadingStages.Length)
        {
            loadingStages[currentStep].SetActive(true); // �ϳ��� Ȱ��ȭ
            currentStep++;
            if (player != null && currentStep == loadingStages.Length)
            {
                player.SetActive(true);
            }
            yield return new WaitForSeconds(delayBetweenSteps);
        }
        yield return new WaitForSeconds(1f); // ��� �ε��� ���� �� 1�� ���
        //introManager.LoadNextScene(); // ���� ������ �̵�
    }
    IEnumerator ScreenChange()
    {
        screen_1.SetActive(true);
        screen_2.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        screen_1.SetActive(false);
        screen_2.SetActive(true);
    }
}
