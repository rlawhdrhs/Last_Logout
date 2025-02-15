using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Scroll : MonoBehaviour
{
    public Camera Camera;
    public float minY = -10.8f;    // 카메라 최소 Y값 (아래쪽)
    public float maxY = 0f;     // 카메라 최대 Y값 (위쪽)


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
