using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SokobanPlayer : MonoBehaviour
{
    public Tilemap tilemapFloor;  // 바닥 타일맵
    public Tilemap tilemapWalls;  // 벽 타일맵
    public float moveDistance = 16f; // 한 번에 이동할 거리 (16픽셀)
    public PlaySound move;
    public PlaySound pull;
    private Vector3Int currentCell; // 현재 위치 (타일 좌표)

    void Start()
    {
        // 현재 위치를 타일 좌표로 변환
        currentCell = tilemapFloor.WorldToCell(transform.position);
        Vector3 newPosition = tilemapFloor.GetCellCenterWorld(currentCell);
        newPosition.y -= 0.12f; // Y축을 0.12만큼 내림
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
        Vector3Int targetCell = currentCell + direction; // 목표 위치 설정


        // 벽이 있는지 확인하고 이동 차단
        if (IsWall(targetCell)) return;

        // 파일 체크
        Collider2D fileCollider = Physics2D.OverlapCircle(tilemapFloor.GetCellCenterWorld(targetCell), 0.1f);
        if (fileCollider != null && fileCollider.CompareTag("File"))
        {
            Sokobanfile file = fileCollider.GetComponent<Sokobanfile>();
            pull.Play();
            if (file != null && file.TryMove(direction))
            {
                // 파일 이동 성공 시 플레이어 이동
                currentCell = targetCell;
                Vector3 newPosition = tilemapFloor.GetCellCenterWorld(currentCell);
                newPosition.y -= 0.12f; // Y축을 0.12만큼 내림
                transform.position = newPosition;
            }
        }
        else
        {
            move.Play();
            // 파일 이동 성공 시 플레이어 이동
            currentCell = targetCell;
            Vector3 newPosition = tilemapFloor.GetCellCenterWorld(currentCell);
            newPosition.y -= 0.12f; // Y축을 0.12만큼 내림
            transform.position = newPosition;
        }
    }

    bool IsWall(Vector3Int cell)
    {
        // 벽 타일맵에서 해당 위치에 타일이 있는지 확인
        return tilemapWalls.HasTile(cell);
    }
}
