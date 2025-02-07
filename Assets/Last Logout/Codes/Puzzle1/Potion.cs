using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healAmount = 1; // 회복량
    public float floatSpeed = 1f; // 떠다니는 속도
    public float floatHeight = 0.2f; // 떠다니는 높이

    private Vector3 startPos; // 시작 위치
    private float timeOffset; // 랜덤한 시작 지점

    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, 2f); // 랜덤한 시간 차이를 줘서 여러 개가 같은 타이밍에 움직이지 않게 함
    }

    void Update()
    {
        // Sin 함수를 이용해 부드럽게 떠다니는 모션
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatHeight, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject); // 포션 제거
            }
        }
    }
}
