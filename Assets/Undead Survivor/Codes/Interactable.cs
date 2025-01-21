using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public PlayerShooting playerShooting;
    public GameObject heartContainer; // ��Ʈ UI �����̳�
    public EnemySpawner enemySpawner;      // EnemySpawner ��ũ��Ʈ ����

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space)) // E Ű�� ��ȣ�ۿ�
        {
            if (heartContainer != null)
            {
                heartContainer.SetActive(true);
            }
            playerShooting.EnableGun(); // �� Ȱ��ȭ
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
