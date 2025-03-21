using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;       // �̵� �ӵ�
    public int health = 3;        // �� ü��
    public int damage = 1;        // �÷��̾�� ���� ���ط�
    private Transform player;     // �÷��̾� ��ġ ����
    public GameObject potionPrefab; // ���� ������
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
        // �÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        if (player != null)
        {
            // �÷��̾ ���� �̵�
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
        // ü�� ����
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
            Puzzle1Manager.instance.DecreaseEnemyCount(); // ���� ������ ���� ����
            Destroy(gameObject); // ü���� 0 ���ϰ� �Ǹ� ����
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
