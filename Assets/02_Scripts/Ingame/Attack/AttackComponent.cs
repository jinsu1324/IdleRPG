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

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(AttackComponentArgs initArgs)
    {
        _attack = initArgs.Attack;
        _attackPower = initArgs.AttackPower;
        _attackSpeed = initArgs.AttackSpeed;

        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsAttackCoolTime())
            Attack();
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
    /// 공격
    /// </summary>
    private void Attack()
    {
        _attack.ExecuteAttack(_attackPower);

        // 공격 애니메이션 재생
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args);
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
}
