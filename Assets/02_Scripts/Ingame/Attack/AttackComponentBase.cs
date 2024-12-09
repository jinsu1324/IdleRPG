using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct OnAttackedArgs
{
    public int AttackSpeed;
}

public abstract class AttackComponentBase : MonoBehaviour
{
    public event Action<OnAttackedArgs> OnAttacked;     // ���� ���� �� �̺�Ʈ

    [SerializeField] protected int _attackMotionFrame;  // ���� ��� ������

    protected int _attackPower;                         // ���ݷ�
    protected int _attackSpeed;                         // ���ݼӵ�
    protected float _attackCooldown;                    // ���� ��Ÿ��
    protected float _time;                              // ��Ÿ�� �ð� ���

    protected float _criticalRate;                      // ġ��Ÿ Ȯ��
    protected float _criticalMultiple;                  // ġ��Ÿ ���� ����
    protected bool _isCritical;                         // ġ��Ÿ ��������

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int attackPower, int attackSpeed, int criticalRate = 0, int criticalMultiple = 0)
    {
        _attackPower = attackPower;
        _attackSpeed = attackSpeed;
        _attackCooldown = 1f / _attackSpeed;
        _criticalRate = ((float)criticalRate / 100.0f);
        _criticalMultiple = ((float)criticalMultiple / 100.0f);
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

        Invoke("Attack", FrameToSecond(AttackTiming()));  // ���ݸ�ǿ� Ÿ�ֿ̹� �°� ���� ����
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    protected abstract void Attack();

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

    /// <summary>
    /// �������� �ʷ� �ٲ��ִ� �Լ�
    /// </summary>
    protected float FrameToSecond(float frame)
    {
        return frame / 60.0f;
    }

    /// <summary>
    /// ����Ÿ�̹��� ����ؼ� ��ȯ���ִ� �Լ�
    /// </summary>
    protected float AttackTiming()
    {
        return _attackMotionFrame / _attackSpeed;
    }

    /// <summary>
    /// ġ��Ÿ�� ����Ͽ� ���� �������� ��ȯ
    /// </summary>
    protected int CalculateDamage()
    {
        bool isCritical = Random.value <= _criticalRate;    // ġ��Ÿ ���� ����

        if (isCritical)
        {
            _isCritical = true;
            return (int)(_attackPower * _criticalMultiple); // ġ��Ÿ ���� ����
        }
        else
        {
            _isCritical = false;
            return _attackPower;
        }
    }
}
