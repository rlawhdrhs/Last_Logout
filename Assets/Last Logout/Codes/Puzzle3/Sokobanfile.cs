using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sokobanfile : MonoBehaviour
{
    public Tilemap tilemapWalls;   // 벽 타일맵
    public Tilemap tilemapFloor;   // 바닥 타일맵

    public bool TryMove(Vector3Int direction)
    {
        Vector3Int targetCell = tilemapFloor.WorldToCell(transform.position) + direction;
        Vector3 targetPosition = tilemapFloor.GetCellCenterWorld(targetCell);

        // 벽 또는 다른 파일 체크
        if (tilemapWalls.HasTile(targetCell))
        {
            Debug.Log("Blocked by Wall or Obstacle"); // 벽이나 장애물이 있을 때 출력
            return false;
        }
        // 이동 가능하면 새로운 위치로 이동
        transform.position = targetPosition;
        return true;
    }
}
