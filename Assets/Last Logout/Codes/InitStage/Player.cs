using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Exit_open) 
                {
                    Exit_open = true;
                    ExitWindow.SetActive(true);
                    movable = false;
                }
                else
                {
                    Exit_open = false;
                    ExitWindow.SetActive(false);
                    movable = true;
                }
            }
        }
        if (MissionList != null && !Exit_open)
        {
            if (Input.GetKeyDown(KeyCode.F))     //의뢰지 켜기
            {
                if (movable)                    //의뢰지가 닫혀있는 경우
                {
                    MissionList.SetActive(true);
                    CheckWindow.SetActive(false);
                    movable = false;
                }
                else                            //의뢰지가 열려있는 경우
                {
                    MissionList.SetActive(false);
                    CheckWindow.SetActive(false);
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
}
