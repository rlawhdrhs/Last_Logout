using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    public GameObject[] loadingStages; // �ε� �ܰ躰 ������Ʈ
    public float delayBetweenSteps = 0.5f; // 0.5�ʸ��� Ȱ��ȭ
    public IntroManager introManager;
    private int currentStep = 0;

    private void Start()
    {
        // ó�� ������ �� ��� �ε� ��������Ʈ�� ����
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
            Debug.Log("Ȱ��ȭ");
            loadingStages[currentStep].SetActive(true); // �ϳ��� Ȱ��ȭ
            currentStep++;
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        yield return new WaitForSeconds(1f); // ��� �ε��� ���� �� 1�� ���
        //introManager.LoadNextScene(); // ���� ������ �̵�
    }
}
