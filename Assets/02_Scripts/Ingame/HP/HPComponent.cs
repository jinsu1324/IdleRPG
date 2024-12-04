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
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // 데미지 받았을때 이벤트
    public Action OnDead;                           // 죽었을 때 이벤트

    public int CurrentHp { get; private set; }      // 현재 체력 
    public int MaxtHp { get; private set; }         // 최대 체력
    public bool IsDead { get; private set; }        // 죽었는지

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(int initHp)
    {
        IsDead = false;
        MaxtHp = initHp;
        CurrentHp = MaxtHp;
    }

    /// <summary>
    /// 데미지 받음
    /// </summary>
    public void TakeDamage(int atk)
    {
        CurrentHp -= atk;
        
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = this.CurrentHp, MaxHp = this.MaxtHp };
        OnTakeDamaged?.Invoke(args);

        if (CurrentHp <= 0)
            Die();
    }

    /// <summary>
    /// 최대체력 변경
    /// </summary>
    public void ChangeMaxHp(int value)
    {
        MaxtHp = value;
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public void Die()
    {
        IsDead = true;
        OnDead?.Invoke();
    }
}
