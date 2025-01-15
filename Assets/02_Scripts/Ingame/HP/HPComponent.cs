using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������ �޾����� �ʿ��� �͵� ����ü
/// </summary>
public struct OnTakeDamagedArgs
{
    public float CurrentHp;
    public float MaxHp;
}

/// <summary>
/// HP������Ʈ ���̽�
/// </summary>
public abstract class HPComponent : MonoBehaviour, IDamagable
{
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // ������ �޾����� �̺�Ʈ
    public float CurrentHp { get; set; }            // ���� ü�� 
    public float MaxHp { get; set; }                // �ִ� ü��
    public bool IsDead { get; set; }                // �׾�����
    
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public virtual void Init(float hp)
    {
        IsDead = false;

        MaxHp = hp;
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void TakeDamage(float atk, bool isCritical)
    {
        // �׾����� �׳� ����
        if (IsDead)
            return;

        // ü�� ���
        CurrentHp -= atk;
        
        // �̺�Ʈ �˸�
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = this.CurrentHp, MaxHp = this.MaxHp };
        OnTakeDamaged?.Invoke(args);

        // ������ �޾����� �½�ũ�� ó��
        TaskDamaged(atk, isCritical);
    }

    /// <summary>
    /// ������ �޾����� �½�ũ�� ó��
    /// </summary>
    protected abstract void TaskDamaged(float atk, bool isCritical);

    /// <summary>
    /// ����
    /// </summary>
    public void Die()
    {
        if (IsDead) 
            return;

        IsDead = true;

        TaskDie();
    }

    /// <summary>
    /// �׾����� �½�ũ�� ó��
    /// </summary>
    protected abstract void TaskDie();
}
