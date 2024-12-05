using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����
/// </summary>
public enum MoveState
{
    Move,
    Stop
}

/// <summary>
/// ������ ����
/// </summary>
public enum MoveDir
{
    Left,
    Right,
    Up,
    Down
}

public class MoveComponent : MonoBehaviour
{
    private MoveDir _moveDir = MoveDir.Left;   // ������ ����
    private MoveState _moveState;              // ������ ����
    private int _moveSpeed;                    // �̵��ӵ�

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _moveState = MoveState.Move;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        switch (_moveState) // ������ ���¿� ���� �����̱� / ���߱�
        {
            case MoveState.Move:
                MoveLinear();
                break;
            case MoveState.Stop:
                StopMove();
                break;
        }
    }

    /// <summary>
    /// �̵�
    /// </summary>
    private void MoveLinear()
    {
        transform.position += GetMoveDir() * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    private void StopMove()
    {
        transform.position = transform.position;
    }
    
    /// <summary>
    /// ������ ���� 'Move'�� ����
    /// </summary>
    public void ChangeMoveState_Move()
    {
        _moveState = MoveState.Move;
    }

    /// <summary>
    /// ������ ���� 'Stop'���� ����
    /// </summary>
    public void ChangeMoveState_Stop()
    {
        _moveState = MoveState.Stop;
    }

    /// <summary>
    /// ������ ���� ��������
    /// </summary>
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
