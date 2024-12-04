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
    public event Action<OnAttackedArgs> OnAttacked;  // ���� ���� �� �̺�Ʈ

    private IAttackable _attack;                     // ����
    private int _attackPower;                        // ���ݷ�
    private int _attackSpeed;                        // ���ݼӵ�

    private float _attackCooldown;                   // ���� ��Ÿ��
    private float _time;                             // ��Ÿ�� �ð� ���

    /// <summary>
    /// �ʱ�ȭ
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
    /// ���� ��Ÿ�� ���
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
    /// ����
    /// </summary>
    private void Attack()
    {
        _attack.ExecuteAttack(_attackPower);

        // ���� �ִϸ��̼� ���
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args);
    }

    /// <summary>
    /// ���ݷ� ����
    /// </summary>
    public void ChangeAttackPower(int value)
    {
        _attackPower = value;
    }

    /// <summary>
    /// ���ݼӵ� ����
    /// </summary>
    public void ChangeAttackSpeed(int value)
    {
        _attackSpeed = value;
        _attackCooldown = 1f / _attackSpeed;
    }
}
