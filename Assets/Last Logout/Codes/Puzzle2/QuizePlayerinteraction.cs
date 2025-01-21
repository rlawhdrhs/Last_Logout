using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizePlayerinteraction : MonoBehaviour
{
    public Transform[] choicePositions; // 선택지 3개 위치 (2,3,4)
    private int currentChoiceIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentChoiceIndex != -1)
        {
            FindObjectOfType<QuizManager>().SelectChoice(currentChoiceIndex);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < choicePositions.Length; i++)
        {
            if (other.transform == choicePositions[i])
            {
                currentChoiceIndex = i;
                break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (currentChoiceIndex != -1 && other.transform == choicePositions[currentChoiceIndex])
        {
            currentChoiceIndex = -1;
        }
    }
}
