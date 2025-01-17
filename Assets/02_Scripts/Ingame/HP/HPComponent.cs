using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// HP������Ʈ �ʱ�ȭ�Ҷ� �ʿ��� �͵� ����ü
/// </summary>
public struct HPInitArgs
{
    public float MaxHp;     // �ִ�ü��
}

/// <summary>
/// ������ �޾����� �ʿ��� �͵� ����ü
/// </summary>
public struct TakeDamageArgs
{
    public float Damage;        // ���� ������
    public bool IsCritical;     // ũ��Ƽ������?
    public float CurrentHp;     // ���� HP
    public float MaxHp;         // �ƽ� HP
}

/// <summary>
/// HP ������Ʈ
/// </summary>
public class HPComponent : MonoBehaviour, IDamagable
{
    public Action<TakeDamageArgs> OnTakeDamage;     // ������ �޾����� �̺�Ʈ
    public Action OnDie;                            // �׾��� �� �̺�Ʈ
    public float CurrentHp { get; set; }            // ���� ü�� 
    public float MaxHp { get; set; }                // �ִ� ü��
    public bool IsDead { get; set; }                // �׾�����
    
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public virtual void Init(HPInitArgs args)
    {
        IsDead = false;

        MaxHp = args.MaxHp;
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void TakeDamage(TakeDamageArgs args)
    {
        if (IsDead) // �׾����� �׳� ����
            return;

        CurrentHp -= args.Damage;

        args.CurrentHp = CurrentHp; // ���� ü�¿� ���� �����鵵 ����ü�� ���
        args.MaxHp = MaxHp;
        
        OnTakeDamage?.Invoke(args); // ������ �޾����� �̺�Ʈ �˸�

        if (CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Die()
    {
        if (IsDead) // �̹� �׾����� ����
            return;

        IsDead = true;
        OnDie?.Invoke(); // �׾����� �̺�Ʈ �˸�
    }

    /// <summary>
    /// �ִ�ü�� ����
    /// </summary>
    public void ChangeMaxHp(PlayerStatArgs args) => MaxHp = args.MaxHp;

    /// <summary>
    /// HP ����
    /// </summary>
    public void ResetHp() => CurrentHp = MaxHp;

    /// <summary>
    /// �׾����� false�� ����
    /// </summary>
    public void ResetIsDead() => IsDead = false;
}
