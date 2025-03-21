using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionRequest : MonoBehaviour
{
    public PlaySound sound;
    // Start is called before the first frame update
    private void Start()
    {
        if(GameManager.instance.isOpenMission)
            sound.Play();
    }
    void Update()
    {
        if (!GameManager.instance.isOpenMission) 
        {
            gameObject.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sound.Play();
            gameObject.SetActive(false);
            GameManager.instance.isOpenMission = false;
        }
    }
}
