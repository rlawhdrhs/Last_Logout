using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeleteUi : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject deleteWindow;
    public SNSManager manager;
    SpriteRenderer sprite;
    Color originColor;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originColor = sprite.color;
    }
    void Update()
    {
        if (isPlayerNearby)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                string portalName = gameObject.name; // 현재 포탈 오브젝트 이름 가져오기

                switch (portalName)
                {
                    case "Delete":
                        deleteWindow.SetActive(true);
                        break;
                    case "Setting":
                        deleteWindow.SetActive(true);
                        break;
                    case "Ok":
                        deleteWindow.SetActive(false);
                        manager.Loading();
                        break;
                    case "Ok_Puzzle3":
                        deleteWindow.SetActive(false);
                        manager.Puzzle3();
                        break;
                    case "No":
                        deleteWindow.SetActive(false);
                        break;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            sprite.color = new Color(originColor.r, originColor.g, originColor.b, 0.78f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            sprite.color = originColor;
        }
    }
}
