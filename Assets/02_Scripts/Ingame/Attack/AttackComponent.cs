using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct OnAttackedArgs
{
    public float AttackSpeed;
}

public abstract class AttackComponent : MonoBehaviour
{
    public event Action<OnAttackedArgs> OnAttacked;     // 공격 했을 때 이벤트

    [SerializeField] protected int _attackMotionFrame;  // 공격 모션 프레임

    protected float _attackPower;                       // 공격력
    protected float _attackSpeed;                       // 공격속도
    protected float _attackCooldown;                    // 공격 쿨타임
    protected float _time;                              // 쿨타임 시간 계산

    /// <summary>
    /// 초기화
    /// </summary>
    public virtual void Init(float attackPower, float attackSpeed)
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
        // 이벤트 노티
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args);

        // 공격모션에 타이밍에 맞게 실제 공격
        Invoke("Attack", FrameToSecond.Convert(AttackTiming()));  
    }

    /// <summary>
    /// 실제 공격
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// 공격타이밍을 계산해서 반환해주는 함수
    /// </summary>
    protected float AttackTiming() => _attackMotionFrame / _attackSpeed;
}
