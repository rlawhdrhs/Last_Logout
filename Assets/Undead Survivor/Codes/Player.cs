using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float runspeed;
    public SpriteRenderer scroll;
    // Start is called before the first frame update

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

    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
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
