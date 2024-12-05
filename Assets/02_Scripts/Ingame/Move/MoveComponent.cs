using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 움직임 상태
/// </summary>
public enum MoveState
{
    Move,
    Stop
}

/// <summary>
/// 움직일 방향
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
    private MoveDir _moveDir = MoveDir.Left;   // 움직일 방향
    private MoveState _moveState;              // 움직임 상태
    private int _moveSpeed;                    // 이동속도

    /// <summary>
    /// 초기화
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
        switch (_moveState) // 움직임 상태에 따라 움직이기 / 멈추기
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
    /// 이동
    /// </summary>
    private void MoveLinear()
    {
        transform.position += GetMoveDir() * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 멈추기
    /// </summary>
    private void StopMove()
    {
        transform.position = transform.position;
    }
    
    /// <summary>
    /// 움직임 상태 'Move'로 변경
    /// </summary>
    public void ChangeMoveState_Move()
    {
        _moveState = MoveState.Move;
    }

    /// <summary>
    /// 움직임 상태 'Stop'으로 변경
    /// </summary>
    public void ChangeMoveState_Stop()
    {
        _moveState = MoveState.Stop;
    }

    /// <summary>
    /// 움직일 방향 가져오기
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
