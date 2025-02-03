using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ���� ���õ� �͵� ������Ʈ�Ҷ� �ʿ��Ѱ͵� ����ü
/// </summary>
public struct AttackUpdateArgs
{
    public float AttackPower;   // ���ݷ�
    public float AttackSpeed;   // ���ݼӵ�
}

/// <summary>
/// ����������Ʈ �ʱ�ȭ�� �ʿ��� �͵� ����ü
/// </summary>
public struct AttackInitArgs
{
    public float AttackPower;   // ���ݷ�
    public float AttackSpeed;   // ���ݼӵ�
}

/// <summary>
/// �����Ҷ� �ʿ��� �͵� ����ü
/// </summary>
public struct AttackArgs
{
    public float AttackPower;           // ���ݷ�
    public float AttackSpeed;           // ���ݼӵ�
    public bool IsCritical;             // ũ��Ƽ������?
    public IDamagable Target;           // Ÿ��
}

/// <summary>
/// ���� ������Ʈ
/// </summary>
public abstract class AttackComponent : MonoBehaviour
{
    public event Action<AttackArgs> OnAttack;           // ���� ���� �� �̺�Ʈ

    [SerializeField] protected int _attackMotionFrame;  // ���� ��� ������

    protected float _attackPower;                       // ���ݷ�
    protected float _attackSpeed;                       // ���ݼӵ�
    protected float _attackCooldown;                    // ���� ��Ÿ��
    protected float _time;                              // ��Ÿ�� �ð� ���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(AttackInitArgs args)
    {
        _attackPower = args.AttackPower;
        _attackSpeed = args.AttackSpeed;
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
        AttackArgs args = new AttackArgs() { AttackSpeed = _attackSpeed };
        OnAttack?.Invoke(args);

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

    /// <summary>
    /// ���ݷ� ������Ʈ
    /// </summary>
    public void Update_AttackPower(AttackUpdateArgs args) => _attackPower = args.AttackPower;

    /// <summary>
    /// ���ݼӵ� ������Ʈ
    /// </summary>
    public void Update_AttackSpeed(AttackUpdateArgs args)
    {
        _attackSpeed = args.AttackSpeed;
        _attackCooldown = 1f / _attackSpeed;
    }
}
