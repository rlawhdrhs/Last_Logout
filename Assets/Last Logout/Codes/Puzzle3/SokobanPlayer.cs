using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SokobanPlayer : MonoBehaviour
{
    public Tilemap tilemapFloor;  // �ٴ� Ÿ�ϸ�
    public Tilemap tilemapWalls;  // �� Ÿ�ϸ�
    public float moveDistance = 16f; // �� ���� �̵��� �Ÿ� (16�ȼ�)
    public PlaySound move;
    public PlaySound pull;
    private Vector3Int currentCell; // ���� ��ġ (Ÿ�� ��ǥ)

    void Start()
    {
        // ���� ��ġ�� Ÿ�� ��ǥ�� ��ȯ
        currentCell = tilemapFloor.WorldToCell(transform.position);
        Vector3 newPosition = tilemapFloor.GetCellCenterWorld(currentCell);
        newPosition.y -= 0.12f; // Y���� 0.12��ŭ ����
        transform.position = newPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Move(Vector3Int.up);
        if (Input.GetKeyDown(KeyCode.S)) Move(Vector3Int.down);
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector3Int.left);
        if (Input.GetKeyDown(KeyCode.D)) Move(Vector3Int.right);
    }

    void Move(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction; // ��ǥ ��ġ ����


        // ���� �ִ��� Ȯ���ϰ� �̵� ����
        if (IsWall(targetCell)) return;

        // ���� üũ
        Collider2D fileCollider = Physics2D.OverlapCircle(tilemapFloor.GetCellCenterWorld(targetCell), 0.1f);
        if (fileCollider != null && fileCollider.CompareTag("File"))
        {
            Sokobanfile file = fileCollider.GetComponent<Sokobanfile>();
            pull.Play();
            if (file != null && file.TryMove(direction))
            {
                // ���� �̵� ���� �� �÷��̾� �̵�
                currentCell = targetCell;
                Vector3 newPosition = tilemapFloor.GetCellCenterWorld(currentCell);
                newPosition.y -= 0.12f; // Y���� 0.12��ŭ ����
                transform.position = newPosition;
            }
        }
        else
        {
            move.Play();
            // ���� �̵� ���� �� �÷��̾� �̵�
            currentCell = targetCell;
            Vector3 newPosition = tilemapFloor.GetCellCenterWorld(currentCell);
            newPosition.y -= 0.12f; // Y���� 0.12��ŭ ����
            transform.position = newPosition;
        }
    }

    bool IsWall(Vector3Int cell)
    {
        // �� Ÿ�ϸʿ��� �ش� ��ġ�� Ÿ���� �ִ��� Ȯ��
        return tilemapWalls.HasTile(cell);
    }
}
