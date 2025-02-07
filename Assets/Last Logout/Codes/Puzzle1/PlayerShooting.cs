using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject gun;           // 총 오브젝트
    public GameObject bulletPrefab;  // 총알 Prefab
    public Transform firePoint;      // 총알 발사 위치
    public float bulletSpeed = 10f;  // 총알 속도
    private bool canShoot = true;   // 발사 가능 여부

    private Vector2 lastDirection = Vector2.right; // 플레이어가 마지막으로 바라본 방향

    void Start()
    {

    }

    void Update()
    {
        // 방향 업데이트: 입력된 이동 방향에 따라 갱신
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputDirection != Vector2.zero)
        {
            lastDirection = inputDirection.normalized; // 마지막 방향 저장
        }

        // 총 발사
        if (canShoot && Input.GetKeyDown(KeyCode.Space)) // 스페이스바로 발사
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알 이동 방향 설정
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = lastDirection * bulletSpeed; // 플레이어의 바라보는 방향으로 발사
        }

        // 총알 회전 설정
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // 외부에서 호출해 총 활성화
    public void EnableGun()
    {
        gun.SetActive(true);   // 총 오브젝트 활성화
        canShoot = true;       // 발사 가능 설정
    }
}
