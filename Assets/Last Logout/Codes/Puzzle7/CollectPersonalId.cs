using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPersonalId : MonoBehaviour
{
    public float floatSpeed = 1f; // 떠다니는 속도
    public float floatHeight = 0.2f; // 떠다니는 높이
    private float timeOffset; // 랜덤한 시작 지점
    public Puzzle7Manager manager;
    public bool isCollected = false;
    public PlaySound sound;
    void Start()
    {
        timeOffset = Random.Range(0f, 2f); // 랜덤한 시간 차이를 줘서 여러 개가 같은 타이밍에 움직이지 않게 함
    }

    void Update()
    {
        // Sin 함수를 이용해 부드럽게 떠다니는 모션
        transform.position = transform.position + new Vector3(0, Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatHeight, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCollected)
        {
            gameObject.SetActive(false); // 추가 충돌 방지
            sound.Play();
            isCollected = true;
            manager.collectPI += 1;
            Destroy(gameObject);
        }
    }
}
