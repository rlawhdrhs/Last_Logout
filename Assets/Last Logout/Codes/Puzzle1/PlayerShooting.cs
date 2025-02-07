using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject gun;           // �� ������Ʈ
    public GameObject bulletPrefab;  // �Ѿ� Prefab
    public Transform firePoint;      // �Ѿ� �߻� ��ġ
    public float bulletSpeed = 10f;  // �Ѿ� �ӵ�
    private bool canShoot = true;   // �߻� ���� ����

    private Vector2 lastDirection = Vector2.right; // �÷��̾ ���������� �ٶ� ����

    void Start()
    {

    }

    void Update()
    {
        // ���� ������Ʈ: �Էµ� �̵� ���⿡ ���� ����
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputDirection != Vector2.zero)
        {
            lastDirection = inputDirection.normalized; // ������ ���� ����
        }

        // �� �߻�
        if (canShoot && Input.GetKeyDown(KeyCode.Space)) // �����̽��ٷ� �߻�
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �Ѿ� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �Ѿ� �̵� ���� ����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = lastDirection * bulletSpeed; // �÷��̾��� �ٶ󺸴� �������� �߻�
        }

        // �Ѿ� ȸ�� ����
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // �ܺο��� ȣ���� �� Ȱ��ȭ
    public void EnableGun()
    {
        gun.SetActive(true);   // �� ������Ʈ Ȱ��ȭ
        canShoot = true;       // �߻� ���� ����
    }
}
