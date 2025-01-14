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
    public int CurrentHp;
    public int MaxHp;
}

/// <summary>
/// HP������Ʈ ���̽�
/// </summary>
public abstract class HPComponent : MonoBehaviour, IDamagable
{
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // ������ �޾����� �̺�Ʈ
    public int CurrentHp { get; set; }              // ���� ü�� 
    public int MaxHp { get; set; }                  // �ִ� ü��
    public bool IsDead { get; private set; }        // �׾�����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public virtual void Init(int hp)
    {
        IsDead = false;

        MaxHp = hp;
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public virtual void TakeDamage(int atk, bool isCritical)
    {
        // �׾����� �׳� ����
        if (IsDead) 
            return;

        // ü�� ���
        CurrentHp -= atk;   
        
        // �̺�Ʈ �˸�
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = this.CurrentHp, MaxHp = this.MaxHp };
        OnTakeDamaged?.Invoke(args);
    }

    /// <summary>
    /// ����
    /// </summary>
    public virtual void Die()
    {
        if (IsDead) 
            return;

        IsDead = true;
    }
}
