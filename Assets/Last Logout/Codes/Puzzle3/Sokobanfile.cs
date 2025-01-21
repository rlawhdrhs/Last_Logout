using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sokobanfile : MonoBehaviour
{
    public Tilemap tilemapWalls;   // �� Ÿ�ϸ�
    public Tilemap tilemapFloor;   // �ٴ� Ÿ�ϸ�

    public bool TryMove(Vector3Int direction)
    {
        Vector3Int targetCell = tilemapFloor.WorldToCell(transform.position) + direction;
        Vector3 targetPosition = tilemapFloor.GetCellCenterWorld(targetCell);

        // �� �Ǵ� �ٸ� ���� üũ
        if (tilemapWalls.HasTile(targetCell))
        {
            Debug.Log("Blocked by Wall or Obstacle"); // ���̳� ��ֹ��� ���� �� ���
            return false;
        }
        // �̵� �����ϸ� ���ο� ��ġ�� �̵�
        transform.position = targetPosition;
        return true;
    }
}
