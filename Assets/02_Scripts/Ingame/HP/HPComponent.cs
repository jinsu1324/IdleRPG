using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnTakeDamagedArgs
{
    public int CurrentHp;
    public int MaxHp;
}


public class HPComponent : MonoBehaviour, IDamagable
{
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // ������ �޾����� �̺�Ʈ

    public int CurrentHp { get; private set; }      // ���� ü�� 
    public int MaxtHp { get; private set; }         // �ִ� ü��

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int initHp)
    {
        MaxtHp = initHp;
        CurrentHp = MaxtHp;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void TakeDamage(int atk)
    {
        CurrentHp -= atk;
        
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = this.CurrentHp, MaxHp = this.MaxtHp };
        OnTakeDamaged?.Invoke(args);
    }

    /// <summary>
    /// �ִ�ü�� ����
    /// </summary>
    public void ChangeMaxHp(int value)
    {
        MaxtHp = value;
    }

    public void Die()
    {
        Debug.Log("�׾����ϴ�.");
    }
}
