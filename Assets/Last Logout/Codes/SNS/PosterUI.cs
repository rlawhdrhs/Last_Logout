using System.Collections;
using UnityEngine;

public class PosterUI : MonoBehaviour
{
    public static PosterUI currentActiveObject = null; // ���� Ȱ��ȭ�� Ʈ����
    private bool isPlayerNearby = false;
    public GameObject Window;
    public bool openHint = false;
    private SpriteRenderer sprite;
    private Color originColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originColor = sprite.color;
    }

    void Update()
    {
        if (isPlayerNearby && currentActiveObject == this)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (gameObject.name)
                {
                    case "Poster":
                        Window.SetActive(true);
                        break;
                    case "Exit":
                        Window.SetActive(false);
                        break;
                    case "Account":
                        StartCoroutine(SendMail());
                        break;
                    case "Hint paper":
                        Window.SetActive(!openHint);
                        openHint = !openHint;
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "Hint paper" && !GameManager.instance.PuzzleClear[4])
            return;
        if (other.CompareTag("Player"))
        {
            if (currentActiveObject != null && currentActiveObject != this)
            {
                // ���� ������Ʈ ��Ȱ��ȭ
                currentActiveObject.isPlayerNearby = false;
                currentActiveObject.sprite.color = currentActiveObject.originColor;
            }

            // ���ο� Ȱ�� ������Ʈ ����
            currentActiveObject = this;
            isPlayerNearby = true;
            sprite.color = new Color(originColor.r * 0.7f, originColor.g * 0.7f, originColor.b * 0.7f, originColor.a);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.name == "Hint paper" && !GameManager.instance.PuzzleClear[4])
            return;
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            sprite.color = originColor;

            if (currentActiveObject == this)
            {
                currentActiveObject = null; // ���� ���õ� ������Ʈ ����
            }
        }
    }

    IEnumerator SendMail()
    {
        Window.SetActive(true);
        yield return new WaitForSeconds(3f);
        Window.SetActive(false);
    }
}
