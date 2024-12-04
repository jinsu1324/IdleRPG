using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDir
{
    Left,
    Right,
    Up,
    Down
}

public class MoveComponent : MonoBehaviour
{
    [Tooltip("������ ������ �����ϼ���")]
    [SerializeField] private MoveDir _moveDir;   // ������ ����
    private int _moveSpeed;                      // �̵��ӵ�

    public void Init(int moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        MoveLinear();
    }

    /// <summary>
    /// �̵�
    /// </summary>
    private void MoveLinear()
    {
        transform.position += GetMoveDir() * _moveSpeed * Time.deltaTime;
    }

    private Vector3 GetMoveDir()
    {
        switch (_moveDir)
        {
            case MoveDir.Left:
                return Vector3.left;
            case MoveDir.Right:
                return Vector3.right;
            case MoveDir.Up:
                return Vector3.up;
            case MoveDir.Down:
                return Vector3.down;
        }

        return Vector3.left;
    }

}
