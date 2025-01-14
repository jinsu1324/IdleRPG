using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct OnAttackedArgs
{
    public int AttackSpeed;
}

public abstract class AttackComponent : MonoBehaviour
{
    public event Action<OnAttackedArgs> OnAttacked;     // ���� ���� �� �̺�Ʈ

    [SerializeField] protected int _attackMotionFrame;  // ���� ��� ������

    protected int _attackPower;                         // ���ݷ�
    protected int _attackSpeed;                         // ���ݼӵ�
    protected float _attackCooldown;                    // ���� ��Ÿ��
    protected float _time;                              // ��Ÿ�� �ð� ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public virtual void Init(int attackPower, int attackSpeed)
    {
        _attackPower = attackPower;
        _attackSpeed = attackSpeed;
        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// ���� ��Ÿ�� ���
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
    /// ���� ���μ��� ����
    /// </summary>
    protected void StartAttackProcess()
    {
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args); // ���� �ִϸ��̼� ���

        Invoke("Attack", FrameToSecond.Convert(AttackTiming()));  // ���ݸ�ǿ� Ÿ�ֿ̹� �°� ���� ����
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// ����Ÿ�̹��� ����ؼ� ��ȯ���ִ� �Լ�
    /// </summary>
    protected float AttackTiming()
    {
        return _attackMotionFrame / _attackSpeed;
    }
}
