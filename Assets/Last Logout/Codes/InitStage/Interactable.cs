using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    private static List<Interactable> activeTriggers = new List<Interactable>(); // 현재 활성화된 트리거 목록
    private bool isPlayerNearby = false;
    private string targetScene;
    private bool openSearch = false;
    public GameObject search;
    public int cur;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public Camera Camera;
    public float minY = -10.8f;    // 카메라 최소 Y값 (아래쪽)
    public float maxY = 0f;     // 카메라 최대 Y값 (위쪽)
    public GameObject Scroll;
    public SpriteRenderer scrollUp, scrollDown; 
    void Start()
    {
        if (!openSearch && gameObject.name == "SearchTrigger")
        {
            search.SetActive(false);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // 원래 색 저장
        }
    }

    void Update()
    {
        if (isPlayerNearby && activeTriggers.Count > 0 && activeTriggers[0] == this)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                string portalName = gameObject.name; // 현재 포탈 오브젝트 이름 가져오기
                Debug.Log(portalName);
                switch (portalName)
                {
                    case "SearchTrigger":
                        targetScene = "Search";
                        break;
                    case "Puzzle1":
                        targetScene = "Puzzle1";
                        break;
                    case "Puzzle2":
                        targetScene = "Puzzle2";
                        break;
                    case "Puzzle3":
                        targetScene = "Puzzle3";
                        break;
                    case "Puzzle4":
                        targetScene = "Puzzle4";
                        break;
                    case "Puzzle5":
                        targetScene = "Puzzle5";
                        break;
                    case "Puzzle6":
                        targetScene = "Puzzle6";
                        break;
                    case "Puzzle7":
                        targetScene = "Puzzle7";
                        break;
                    case "Mail":
                        targetScene = "Mail";
                        break;
                    case "Up":
                        targetScene = "Up";
                        break;
                    case "Down":
                        targetScene = "Down";
                        break;
                    default:
                        targetScene = "DefaultScene";
                        break;
                }
                Interact(targetScene);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (!activeTriggers.Contains(this))
                activeTriggers.Add(this);
            UpdateTriggerSelection();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            activeTriggers.Remove(this);
            UpdateTriggerSelection();
        }
    }

    void UpdateTriggerSelection()
    {
        // **모든 트리거의 색상을 원래대로 되돌림**
        foreach (var trigger in FindObjectsOfType<Interactable>())
        {
            trigger.SetHighlight(false);
        }

        // **가장 가까운 트리거를 찾아서 활성화**
        if (activeTriggers.Count > 0)
        {
            activeTriggers.Sort((a, b) =>
                Vector2.Distance(a.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)
                .CompareTo(Vector2.Distance(b.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position))
            );

            // 리스트의 첫 번째 요소(가장 가까운 트리거)만 활성화
            activeTriggers[0].SetHighlight(true);
        }
    }

    void SetHighlight(bool active)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = active
                ? new Color(originalColor.r * 0.7f, originalColor.g * 0.7f, originalColor.b * 0.7f, originalColor.a)
                : originalColor;
        }
    }

    void Interact(string interactName)
    {
        switch (interactName)
        {
            case "Search":
                openSearch = !openSearch;
                search.SetActive(openSearch);
                break;
            case "Up":
                ScrollUp();
                StartCoroutine(BlinkEffect(scrollUp));
                UpdateScroll();
                break;
            case "Down":
                ScrollDown();
                StartCoroutine(BlinkEffect(scrollDown));
                UpdateScroll();
                break;
            default:
                cur = (int)interactName[6] - '1';
                if (!GameManager.instance.IsPuzzleCleared(cur))
                    SceneManager.LoadScene(interactName + " Stage1");
                break;
        }
    }

    void ScrollUp()
    {
        float newY = Mathf.Clamp(Camera.transform.position.y + 0.2f, minY, maxY);
        Camera.transform.position = new Vector3(Camera.transform.position.x, newY, Camera.transform.position.z);
    }
    void ScrollDown()
    {
        float newY = Mathf.Clamp(Camera.transform.position.y - 0.2f, minY, maxY);
        Camera.transform.position = new Vector3(Camera.transform.position.x, newY, Camera.transform.position.z);
    }
    void UpdateScroll()
    {
        float diff = maxY - minY; // 카메라가 이동할 수 있는 최대 범위
        float percentage = (Camera.transform.position.y - minY) / diff; // 현재 카메라 위치의 퍼센트 (0 ~ 1)
        float newY = Mathf.Lerp(-2.8f, 2.35f, percentage); // 스크롤바 이동 가능 범위에 맞춰 변환
        Scroll.transform.position = new Vector3(Scroll.transform.position.x, newY, Scroll.transform.position.z);
    }
    IEnumerator BlinkEffect(SpriteRenderer sprite)
    {
        Color color = sprite.color;           // 현재 색상 가져오기
        color.a = 0.3f;               // alpha 값을 0~1 사이로 제한 (0 = 완전 투명, 1 = 불투명)
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f); // 0.1초 기다림
        color.a = 0;
        spriteRenderer.color = color;
    }
}
