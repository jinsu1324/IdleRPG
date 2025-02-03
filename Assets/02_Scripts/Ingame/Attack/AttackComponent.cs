using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 공격 관련된 것들 업데이트할때 필요한것들 구조체
/// </summary>
public struct AttackUpdateArgs
{
    public float AttackPower;   // 공격력
    public float AttackSpeed;   // 공격속도
}

/// <summary>
/// 공격컴포넌트 초기화에 필요한 것들 구조체
/// </summary>
public struct AttackInitArgs
{
    public float AttackPower;   // 공격력
    public float AttackSpeed;   // 공격속도
}

/// <summary>
/// 공격할때 필요한 것들 구조체
/// </summary>
public struct AttackArgs
{
    public float AttackPower;           // 공격력
    public float AttackSpeed;           // 공격속도
    public bool IsCritical;             // 크리티컬인지?
    public IDamagable Target;           // 타겟
}

/// <summary>
/// 공격 컴포넌트
/// </summary>
public abstract class AttackComponent : MonoBehaviour
{
    public event Action<AttackArgs> OnAttack;           // 공격 했을 때 이벤트

    [SerializeField] protected int _attackMotionFrame;  // 공격 모션 프레임

    protected float _attackPower;                       // 공격력
    protected float _attackSpeed;                       // 공격속도
    protected float _attackCooldown;                    // 공격 쿨타임
    protected float _time;                              // 쿨타임 시간 계산

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(AttackInitArgs args)
    {
        _attackPower = args.AttackPower;
        _attackSpeed = args.AttackSpeed;
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
        AttackArgs args = new AttackArgs() { AttackSpeed = _attackSpeed };
        OnAttack?.Invoke(args);

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

    /// <summary>
    /// 공격력 업데이트
    /// </summary>
    public void Update_AttackPower(AttackUpdateArgs args) => _attackPower = args.AttackPower;

    /// <summary>
    /// 공격속도 업데이트
    /// </summary>
    public void Update_AttackSpeed(AttackUpdateArgs args)
    {
        _attackSpeed = args.AttackSpeed;
        _attackCooldown = 1f / _attackSpeed;
    }
}
