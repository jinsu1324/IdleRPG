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
    public event Action<OnAttackedArgs> OnAttacked;     // ���� ���� �� �̺�Ʈ

    [SerializeField] protected int _attackMotionFrame;  // ���� ��� ������

    protected float _attackPower;                       // ���ݷ�
    protected float _attackSpeed;                       // ���ݼӵ�
    protected float _attackCooldown;                    // ���� ��Ÿ��
    protected float _time;                              // ��Ÿ�� �ð� ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public virtual void Init(float attackPower, float attackSpeed)
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
        // �̺�Ʈ ��Ƽ
        OnAttackedArgs args = new OnAttackedArgs() { AttackSpeed = _attackSpeed };
        OnAttacked?.Invoke(args);

        // ���ݸ�ǿ� Ÿ�ֿ̹� �°� ���� ����
        Invoke("Attack", FrameToSecond.Convert(AttackTiming()));  
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// ����Ÿ�̹��� ����ؼ� ��ȯ���ִ� �Լ�
    /// </summary>
    protected float AttackTiming() => _attackMotionFrame / _attackSpeed;
}
