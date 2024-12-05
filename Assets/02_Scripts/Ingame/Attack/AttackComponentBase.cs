using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnAttackedArgs
{
    public int AttackSpeed;
}

public abstract class AttackComponentBase : MonoBehaviour
{
    public event Action<OnAttackedArgs> OnAttacked;     // 공격 했을 때 이벤트

    [SerializeField] protected int _attackMotionFrame;  // 공격 모션 프레임

    protected int _attackPower;                         // 공격력
    protected int _attackSpeed;                         // 공격속도
    protected float _attackCooldown;                    // 공격 쿨타임
    protected float _time;                              // 쿨타임 시간 계산

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(int attackPower, int attackSpeed)
    {
        _attackPower = attackPower;
        _attackSpeed = attackSpeed;

        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// 공격 쿨타임 계산
    /// </summary>
    protected bool IsAttackCoolTime()
    {
        _time += Time.deltaTime;

        if (_time >= _attackCooldown)
        {
            _time %= _attackCooldown;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// 공격 프로세스 시작
    /// </summary>
    protected void StartAttackProcess()
    {
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args); // 공격 애니메이션 재생

        Invoke("Attack", FrameToSecond(AttackTiming()));  // 공격모션에 타이밍에 맞게 실제 공격
    }

    /// <summary>
    /// 실제 공격
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// 공격력 변경
    /// </summary>
    public void ChangeAttackPower(int value)
    {
        _attackPower = value;
    }

    /// <summary>
    /// 공격속도 변경
    /// </summary>
    public void ChangeAttackSpeed(int value)
    {
        _attackSpeed = value;
        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// 프레임을 초로 바꿔주는 함수
    /// </summary>
    protected float FrameToSecond(float frame)
    {
        return frame / 60.0f;
    }

    /// <summary>
    /// 공격타이밍을 계산해서 반환해주는 함수
    /// </summary>
    protected float AttackTiming()
    {
        return _attackMotionFrame / _attackSpeed;
    }
}
