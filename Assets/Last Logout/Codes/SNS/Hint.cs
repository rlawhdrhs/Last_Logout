using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.U2D;

public class Hint : MonoBehaviour
{
    public static PosterUI currentActiveObject = null; // 현재 활성화된 트리거
    private bool isPlayerNearby = false;
    public GameObject Window;
    public bool openHint = false;
    private SpriteRenderer sprite;
    private Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originColor = new Color(1,1,1,1);

        if (gameObject.name == "Hint paper" && !GameManager.instance.PuzzleClear[4])
        {
            sprite.color = new Color(0, 0, 0, 0);
        }
        else if (gameObject.name == "Hint paper" && GameManager.instance.PuzzleClear[4])
        {
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (openHint)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Window.SetActive(false);
                openHint = false;
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Window.SetActive(true);
                    openHint = true;
                }
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.instance.PuzzleClear[4])
            return;
        isPlayerNearby = true;
        sprite.color = new Color(originColor.r * 0.7f, originColor.g * 0.7f, originColor.b * 0.7f, originColor.a);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!GameManager.instance.PuzzleClear[4])
            return;
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            sprite.color = originColor;
        }
    }
}
