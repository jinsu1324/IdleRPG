using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct OnTakeDamagedArgs
{
    public int CurrentHp;
    public int MaxHp;
}


public class HPComponent : MonoBehaviour, IDamagable
{
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // ������ �޾����� �̺�Ʈ
    public Action OnDead;                           // �׾��� �� �̺�Ʈ
    public int CurrentHp { get; private set; }      // ���� ü�� 
    public int MaxtHp { get; private set; }         // �ִ� ü��
    public bool IsDead { get; private set; }        // �׾�����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int initHp)
    {
        IsDead = false;
        MaxtHp = initHp;
        CurrentHp = MaxtHp;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void TakeDamage(int atk, bool isCritical)
    {
        if (IsDead) 
            return;

        CurrentHp -= atk;
        
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = this.CurrentHp, MaxHp = this.MaxtHp };
        OnTakeDamaged?.Invoke(args); // ������ �޾����� �̺�Ʈ ����

        DamageTextManager.ShowDamageText(atk, transform.position, isCritical); // ������ �ؽ�Ʈ ����

        if (CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// �ִ�ü�� ����
    /// </summary>
    public void ChangeMaxHp(PlayerStatArgs args)
    {
        MaxtHp = args.MaxHp;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Die()
    {
        if (IsDead) 
            return;

        IsDead = true;
        OnDead?.Invoke();
    }
}
