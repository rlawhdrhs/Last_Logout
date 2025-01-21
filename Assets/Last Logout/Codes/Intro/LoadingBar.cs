using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    public GameObject[] loadingStages; // 로딩 단계별 오브젝트
    public float delayBetweenSteps = 0.5f; // 0.5초마다 활성화
    public IntroManager introManager;
    private int currentStep = 0;

    private void Start()
    {
        // 처음 시작할 때 모든 로딩 스프라이트를 숨김
        HideAllLoadingStages();
        StartLoading();
    }

    private void HideAllLoadingStages()
    {
        foreach (GameObject stage in loadingStages)
        {
            stage.SetActive(false); // 모든 로딩 바 숨기기
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
            Debug.Log("활성화");
            loadingStages[currentStep].SetActive(true); // 하나씩 활성화
            currentStep++;
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        yield return new WaitForSeconds(1f); // 모든 로딩이 끝난 후 1초 대기
        //introManager.LoadNextScene(); // 다음 씬으로 이동
    }
}
