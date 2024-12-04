using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnAttackedArgs
{
    public int AttackSpeed; 
}

public struct AttackComponentArgs
{
    public IAttackable Attack;
    public int AttackPower;
    public int AttackSpeed;
}

public class AttackComponent : MonoBehaviour
{
    public event Action<OnAttackedArgs> OnAttacked;  // 공격 했을 때 이벤트

    private IAttackable _attack;                     // 공격
    private int _attackPower;                        // 공격력
    private int _attackSpeed;                        // 공격속도

    private float _attackCooldown;                   // 공격 쿨타임
    private float _time;                             // 쿨타임 시간 계산
    private int _attackMotionFrame;                  // 공격 모션 프레임

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(AttackComponentArgs initArgs, int attackMotionFrame)
    {
        _attack = initArgs.Attack;
        _attackPower = initArgs.AttackPower;
        _attackSpeed = initArgs.AttackSpeed;

        _attackMotionFrame = attackMotionFrame;

        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsAttackCoolTime())
            StartAttackProcess();
    }

    /// <summary>
    /// 공격 쿨타임 계산
    /// </summary>
    private bool IsAttackCoolTime()
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
    private void StartAttackProcess()
    {
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args); // 공격 애니메이션 재생

        Invoke("Attack", FrameToSecond(AttackTiming()));  // 공격모션에 타이밍에 맞게 실제 공격
    }
    
    /// <summary>
    /// 실제 공격
    /// </summary>
    private void Attack()
    {
        _attack.ExecuteAttack(_attackPower);
    }

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
    private float FrameToSecond(float frame)
    {
        return frame / 60.0f;
    }

    /// <summary>
    /// 공격타이밍을 계산해서 반환해주는 함수
    /// </summary>
    private float AttackTiming()
    {
        return _attackMotionFrame / _attackSpeed;
    }
}
