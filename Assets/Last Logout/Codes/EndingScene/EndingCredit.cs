using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    public GameObject mail;
    public GameObject Exit;
    public bool openmail = true;
    public PlaySound sound;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (openmail)
            {
                mail.SetActive(false);
                openmail = false;
                sound.Play();
            }
            else
            {
                Exit.SetActive(true);
            }
        }
    }
}
