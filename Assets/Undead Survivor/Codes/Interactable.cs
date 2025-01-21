using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public PlayerShooting playerShooting;
    public GameObject heartContainer; // 하트 UI 컨테이너
    public EnemySpawner enemySpawner;      // EnemySpawner 스크립트 참조

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space)) // E 키로 상호작용
        {
            if (heartContainer != null)
            {
                heartContainer.SetActive(true);
            }
            playerShooting.EnableGun(); // 총 활성화
            if (enemySpawner != null)
            {
                enemySpawner.ActivateSpawner();
            }
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
