using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterUI : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public GameObject Window;
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
                    case "Poster_1":
                        Window.SetActive(true);
                        break;
                    case "Poster_2":
                        Window.SetActive(true);
                        break;
                    case "Exit":
                        Window.SetActive(false);
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
            sprite.color = new Color(originColor.r * 0.7f, originColor.g * 0.7f, originColor.b * 0.7f, originColor.a);
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
