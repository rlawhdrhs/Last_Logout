using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float runspeed;
    public SpriteRenderer scroll;
    public GameObject MissionList;
    public GameObject CheckWindow;
    public GameObject ExitWindow;
    // Start is called before the first frame update
    public bool movable = true;
    public bool Check_open = false;
    public bool Exit_open = false;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    public PlaySound run;
    public PlaySound move;
    public PlaySound openM;

    private bool isMoving = false;
    private bool isRunning = false;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.moveToPortal)
            {
                transform.position = new Vector3(1.8f, 4.1f, 0);
                GameManager.instance.moveToPortal = false;
            }
            if(GameManager.instance.currentPuzzle == 1 && SceneManager.GetActiveScene().name == "SNS")
            {
                transform.position = new Vector3(3.68f, 1.68f, 0);
                GameManager.instance.currentPuzzle = 0;
            }
            else if (GameManager.instance.currentPuzzle == 2 && SceneManager.GetActiveScene().name == "GameScene")
            {
                transform.position = new Vector3(-7.3f, 3f, 0);
                GameManager.instance.currentPuzzle = 0;
            }
            else if(GameManager.instance.currentPuzzle == 3 && SceneManager.GetActiveScene().name == "SNS Stage1")
            {
                transform.position = new Vector3(-3.3f, 2.05f, 0);
                GameManager.instance.currentPuzzle = 0;
            }
            else if (GameManager.instance.currentPuzzle == 5 && SceneManager.GetActiveScene().name == "SNS")
            {
                transform.position = new Vector3(-6f, 2.1f, 0);
                GameManager.instance.currentPuzzle = 0;
            }
            else
            {
                transform.position = new Vector3(0, 0, 0);
                GameManager.instance.currentPuzzle = 0;
            }
        }
        if (MissionList != null)
        {
            MissionList.SetActive(false);
        }
        if (CheckWindow != null) 
        {
            ExitWindow.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movable)            //의뢰지가 닫혀있을 때만 이동 가능
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            isMoving = (inputVec.x != 0 || inputVec.y != 0);
            isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

            HandleFootstepSound();
        }
        else
        {
            inputVec = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && movable)
        {
            if (!Exit_open)
            {
                Exit_open = true;
                openM.Play();
                ExitWindow.SetActive(true);
            }
            else
            {
                openM.Play();
                Exit_open = false;
                ExitWindow.SetActive(false);
            }
        }
        if (MissionList != null && !Exit_open)
        {
            if (Input.GetKeyDown(KeyCode.F))     //의뢰지 켜기
            {
                if (movable)                    //의뢰지가 닫혀있는 경우
                {
                    openM.Play();
                    MissionList.SetActive(true);
                    CheckWindow.SetActive(false);
                    movable = false;
                }
                else                            //의뢰지가 열려있는 경우
                {
                    MissionList.SetActive(false);
                    CheckWindow.SetActive(false);
                    openM.Play();
                    movable = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))   //의뢰지가 켜진 상태에서 게임 종료
            {
                if (!movable)
                {
                    if (!Check_open)
                    {
                        CheckWindow.SetActive(true);
                        Check_open = true;
                    }
                    else
                    {
                        CheckWindow.SetActive(false);
                        Check_open = false;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        //Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        //rigid.MovePosition(rigid.position + nextVec);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runspeed : speed;

        // 플레이어 이동 처리
        rigid.MovePosition(rigid.position + inputVec.normalized * currentSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0 ? true : false;
            scroll.flipX = inputVec.x < 0 ? true : false;
        }
    }

    void HandleFootstepSound()
    {
        if (isMoving)
        {
            if (isRunning)
            {
                if (!run.audioSource.isPlaying)
                {
                    run.Play();
                    move.StopSound();
                }
            }
            else
            {
                if (!move.audioSource.isPlaying)
                {
                    move.Play();
                    run.StopSound();
                }
            }
        }
        else
        {
            StopFootstepSound();
        }
    }

    void StopFootstepSound()
    {
        if (move != null)
            move.StopSound();
        if (run != null)
            run.StopSound();
    }
}
