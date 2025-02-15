using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Scroll : MonoBehaviour
{
    public Camera Camera;
    public float minY = -10.8f;    // ī�޶� �ּ� Y�� (�Ʒ���)
    public float maxY = 0f;     // ī�޶� �ִ� Y�� (����)


    void ScrollUp()
    {
        float newY = Mathf.Lerp(minY, maxY, Camera.transform.position.y - 0.5f);
        Camera.transform.position = new Vector3(Camera.transform.position.x, newY, Camera.transform.position.z);
    }
    void ScrollDown()
    {
        float newY = Mathf.Lerp(minY, maxY, Camera.transform.position.y + 0.5f);
        Camera.transform.position = new Vector3(Camera.transform.position.x, newY, Camera.transform.position.z);
    }
}
