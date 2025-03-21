using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;       // 이동 속도
    public int health = 3;        // 적 체력
    public int damage = 1;        // 플레이어에게 가할 피해량
    private Transform player;     // 플레이어 위치 참조
    public GameObject potionPrefab; // 포션 프리팹
    public Puzzle1Manager puzzle1Manager;
    Animator anim;
    Rigidbody2D rigid;
    WaitForFixedUpdate wait;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        wait = new WaitForFixedUpdate();
        anim = GetComponent<Animator>();
        puzzle1Manager = FindAnyObjectByType<Puzzle1Manager>();
    }
    void Start()
    {
        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        if (player != null)
        {
            // 플레이어를 향해 이동
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 nextVec = direction * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }

    void OnEnable()
    {
        anim.SetBool("Dead", false);
    }

    public void TakeDamage(int damage)
    {
        // 체력 감소
        StartCoroutine(KnockBack());
        health -= damage;

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            GameObject enemy = GameObject.Find("enemyS");
            PlaySound sound = enemy.GetComponent<PlaySound>();
            sound.Play();
            DropPotion();
            anim.SetBool("Dead", true);
            Puzzle1Manager.instance.DecreaseEnemyCount(); // 적이 죽으면 개수 감소
            Destroy(gameObject); // 체력이 0 이하가 되면 제거
        }
    }


    IEnumerator KnockBack()
    {
        yield return wait;
        Vector2 direction = (transform.position - player.position).normalized;
        rigid.AddForce(direction * 2, ForceMode2D.Impulse);
    }
    void DropPotion()
    {
        if (potionPrefab != null && puzzle1Manager.enemyCount == 45)
        {
            Instantiate(potionPrefab, transform.position, Quaternion.identity);
        }
    }
}
